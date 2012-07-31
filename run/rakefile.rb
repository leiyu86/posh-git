require 'rake'
require 'albacore'
require 'securerandom'
require 'fileutils'
require './ruby/gallio.rb'
require 'win32/registry'
  
Dir.chdir('..')
  
PROGRAMM_FILES = ENV['ProgramFiles']
ROOT = Dir.pwd
SRC = File.join(ROOT, 'src')
BIN = File.join(ROOT, 'bin')
LIB = File.join(ROOT, 'lib')
CONFIG = File.join(ROOT, 'run', 'config')
 
FRAMEWORK = 'Wonga.QA.Framework'
TESTS = 'Wonga.QA.Tests'
SERVICETESTS = 'Wonga.QA.ServiceTests'
DATATESTS = 'Wonga.QA.DataTests'
UITESTS = 'Wonga.QA.UiTests'
TOOLS = 'Wonga.QA.Tools'
 
def get_MSBuildToolsPath
  Win32::Registry::open(Win32::Registry::HKEY_LOCAL_MACHINE, 'SOFTWARE\\Microsoft\\MSBuild\\ToolsVersions\\4.0') do |reg|
    type, value = reg.read('MSBuildToolsPath')
    value
  end
end

def get_full_dll_path(name)
  unless name == 'Ui'
    file = File.join(BIN, "#{TESTS}.#{name}.dll")
  else
    file = File.join(BIN, "#{UITESTS}.#{name}.dll")
  end
  file
end

def get_dlls_list(names)
  if names and not names.empty?
    list = names.split(';')
    list.uniq!
    list.collect! { |name| name = get_full_dll_path name   }
  else
    list = Array.new
  end
  list
end

task :pre_test_cleanup do 
  bin_dir = Dir.new(BIN)
  config_files_to_delete = Dir.glob("*.v3qaconfig")
  FileUtils.rm config_files_to_delete
  puts 'cleanup'
end
  
task :post_test_cleanup do #should be extended
  bin_dir = Dir.new(BIN)
  config_files_to_delete = bin_dir.glob("*.v3qaconfig")
  FileUtils.rm config_files_to_delete
end
  
task :config do |t, arg| #ready
  test_target = ENV['test_target']
  FileUtils.cp File.join(CONFIG, test_target,'.v3qaconfig'), BIN unless test_target.empty?
  puts "test target: #{test_target}"
end
  
task :pre_generate_serializers do #ready
	puts 'Generating serializers'
	cmd = Exec.new
  cmd.command = File.join(LIB,'sgen','sgen.exe')
  cmd.parameters = File.join(BIN,'Wonga.QA.Framework.Cs.dll') + ' /force'
	cmd.execute  
end
#--
  
task :merge do #ready
  exclude = File.join(BIN, "#{TESTS}.Core.dll")
  exclude += ' ' + File.join(BIN, "#{TESTS}.Meta.dll")
  exclude += ' ' + File.join(BIN, "#{TESTS}.Ui.dll")
  exclude += ' ' + File.join(BIN, "#{TESTS}.Ui.Mobile.dll")
  exclude += ' ' + File.join(BIN, "#{TESTS}.Migration.dll")
  exclude_dlls = exclude.split(' ')
  
  Dir.chdir(BIN)

  include = Dir.glob(TESTS + '.*.dll')
  include.each { |item| item.insert(0, BIN + '\\')}
  include_dlls = include - exclude_dlls
    
  command = File.join(PROGRAMM_FILES, 'Microsoft', 'ILMerge','ILMerge.exe')
    
  params = '/targetplatform:v4,'+get_MSBuildToolsPath#read_msbuild_property("MSBuildToolsPath")
  params += ' /out:' + File.join(BIN, "#{TESTS}.dll")
  #params += ' /allowDup' 
  #uncomment the row above to be able to merge
  params += ' '+ File.join(BIN, "#{TESTS}.Core.dll")
  include_dlls.each { |dll| params+= ' ' + dll }
        
	cmd = Exec.new
  cmd.command = command
  cmd.parameters = params
	cmd.execute
end
  
desc 'run Gallio'
gallio :gallio, :test_dlls, :test_filter do |g, dlls, fltr| 
  if dlls and not dlls.empty?
    dlls.each { |dll| g.addTestAssembly(dlls) }
  else g.addTestAssembly(File.join(BIN, "#{TESTS}.dll"))
  end
  g.filter = fltr if fltr and not fltr.empty? 
  g.reportDirectory = File.join(BIN, "#{TESTS}.Report")
  g.reportNameFormat = "#{TESTS}.Report"
  g.addReportType("xml")
end

task :test, :include, :exclude, :test_filter do |t, incl, excl, fltr|
  args.with_defaults(:include => ENV['include'], :exclude => ENV['exclude'], :test_filter => ENV['test_filter'])
  include = get_dlls_list incl
  exclude = get_dlls_list excl
  test_dlls = include - exclude
  Rake::Task[:gallio].invoke(test_dlls, fltr)
  Rake::Task[:convert_reports]

end
  
task :convert_reports do
	command = File.join(BIN,'Wonga.QA.Tools.ReportConverter','Wonga.QA.Tools.ReportConverter.exe')
	params1 = '"' + File.join(BIN, "#{TESTS}.Report", "#{TESTS}.Report.xml\" ") 
	params1 += '"' + File.join(BIN, "#{TESTS}.Report", "#{TESTS}.Report.html\" ")
	params1 += 'html'
	
	params2 = '"' + File.join(BIN, "#{TESTS}.Report", "#{TESTS}.Report.xml\" ")
	params2 += '"' + File.join(BIN, "#{TESTS}.Report", "#{TESTS}.Report.csv\" ")
	params2 += 'csv'
	
	sh command + ' ' + params1
	sh command + ' ' + params2
end
  
#-- build tasks for each solution  
msbuild :framework do |msb|
  msb.solution = File.join(SRC, FRAMEWORK,  "#{FRAMEWORK}.sln")
end
  
msbuild :tests do |msb|
  msb.solution = File.join(SRC, TESTS, "#{TESTS}.sln")
end
  
msbuild :service_tests do |msb|
  msb.solution = File.join(SRC, SERVICETESTS, "#{SERVICETESTS}.sln")
end
  
msbuild :data_tests do |msb|
  msb.solution = File.join(SRC, DATATESTS, "#{DATATESTS}.sln")
end

msbuild :ui_tests do |msb|
  msb.solution = File.join(SRC, UITESTS, "#{UITESTS}.sln")
end
  
msbuild :tools do |msb|
  msb.solution = File.join(SRC, TOOLS, "#{TOOLS}.sln")
end
#--
  
#--Task dependencies
task :default => [:config, :build, :merge, :pre_generate_serializers]
  
task :build => [:framework, :tests, :ui_tests, :service_tests, :data_tests, :tools]
#--

