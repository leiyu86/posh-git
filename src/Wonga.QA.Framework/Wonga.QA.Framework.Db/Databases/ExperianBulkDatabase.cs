#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wonga.QA.Framework.Db.ExperianBulk
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="ExperianBulk")]
	public partial class ExperianBulkDatabase : DbDatabase<ExperianBulkDatabase>
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCustomerEntity(CustomerEntity instance);
    partial void UpdateCustomerEntity(CustomerEntity instance);
    partial void DeleteCustomerEntity(CustomerEntity instance);
    partial void InsertFactorEntity(FactorEntity instance);
    partial void UpdateFactorEntity(FactorEntity instance);
    partial void DeleteFactorEntity(FactorEntity instance);
    partial void InsertMSSQLDeploy(MSSQLDeploy instance);
    partial void UpdateMSSQLDeploy(MSSQLDeploy instance);
    partial void DeleteMSSQLDeploy(MSSQLDeploy instance);
    partial void InsertOperationEntity(OperationEntity instance);
    partial void UpdateOperationEntity(OperationEntity instance);
    partial void DeleteOperationEntity(OperationEntity instance);
    partial void InsertParsedDataEntity(ParsedDataEntity instance);
    partial void UpdateParsedDataEntity(ParsedDataEntity instance);
    partial void DeleteParsedDataEntity(ParsedDataEntity instance);
    partial void InsertParsedDataTempEntity(ParsedDataTempEntity instance);
    partial void UpdateParsedDataTempEntity(ParsedDataTempEntity instance);
    partial void DeleteParsedDataTempEntity(ParsedDataTempEntity instance);
    partial void InsertTypeEntity(TypeEntity instance);
    partial void UpdateTypeEntity(TypeEntity instance);
    partial void DeleteTypeEntity(TypeEntity instance);
    #endregion
		
		public ExperianBulkDatabase(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ExperianBulkDatabase(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ExperianBulkDatabase(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ExperianBulkDatabase(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CustomerEntity> Customers
		{
			get
			{
				return this.GetTable<CustomerEntity>();
			}
		}
		
		public System.Data.Linq.Table<FactorEntity> Factors
		{
			get
			{
				return this.GetTable<FactorEntity>();
			}
		}
		
		public System.Data.Linq.Table<MSSQLDeploy> MSSQLDeploys
		{
			get
			{
				return this.GetTable<MSSQLDeploy>();
			}
		}
		
		public System.Data.Linq.Table<OperationEntity> Operations
		{
			get
			{
				return this.GetTable<OperationEntity>();
			}
		}
		
		public System.Data.Linq.Table<ParsedDataEntity> ParsedDatas
		{
			get
			{
				return this.GetTable<ParsedDataEntity>();
			}
		}
		
		public System.Data.Linq.Table<ParsedDataTempEntity> ParsedDataTemps
		{
			get
			{
				return this.GetTable<ParsedDataTempEntity>();
			}
		}
		
		public System.Data.Linq.Table<TypeEntity> Types
		{
			get
			{
				return this.GetTable<TypeEntity>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="experian.Customers")]
	public partial class CustomerEntity : DbEntity<CustomerEntity>, INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CustomerId;
		
		private System.Guid _AccountId;
		
		private string _Name;
		
		private string _AddressDetails1;
		
		private string _AddressDetails2;
		
		private string _AddressDetails3;
		
		private string _AddressDetails4;
		
		private string _PostCode;
		
		private System.DateTime _DateOfBirth;
		
		private System.DateTime _LastUpdatedDate;
		
		private System.DateTime _CreatedDate;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCustomerIdChanging(int value);
    partial void OnCustomerIdChanged();
    partial void OnAccountIdChanging(System.Guid value);
    partial void OnAccountIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnAddressDetails1Changing(string value);
    partial void OnAddressDetails1Changed();
    partial void OnAddressDetails2Changing(string value);
    partial void OnAddressDetails2Changed();
    partial void OnAddressDetails3Changing(string value);
    partial void OnAddressDetails3Changed();
    partial void OnAddressDetails4Changing(string value);
    partial void OnAddressDetails4Changed();
    partial void OnPostCodeChanging(string value);
    partial void OnPostCodeChanged();
    partial void OnDateOfBirthChanging(System.DateTime value);
    partial void OnDateOfBirthChanged();
    partial void OnLastUpdatedDateChanging(System.DateTime value);
    partial void OnLastUpdatedDateChanged();
    partial void OnCreatedDateChanging(System.DateTime value);
    partial void OnCreatedDateChanged();
    #endregion
		
		public CustomerEntity()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int CustomerId
		{
			get
			{
				return this._CustomerId;
			}
			set
			{
				if ((this._CustomerId != value))
				{
					this.OnCustomerIdChanging(value);
					this.SendPropertyChanging();
					this._CustomerId = value;
					this.SendPropertyChanged("CustomerId");
					this.OnCustomerIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccountId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				if ((this._AccountId != value))
				{
					this.OnAccountIdChanging(value);
					this.SendPropertyChanging();
					this._AccountId = value;
					this.SendPropertyChanged("AccountId");
					this.OnAccountIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AddressDetails1", DbType="VarChar(100)")]
		public string AddressDetails1
		{
			get
			{
				return this._AddressDetails1;
			}
			set
			{
				if ((this._AddressDetails1 != value))
				{
					this.OnAddressDetails1Changing(value);
					this.SendPropertyChanging();
					this._AddressDetails1 = value;
					this.SendPropertyChanged("AddressDetails1");
					this.OnAddressDetails1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AddressDetails2", DbType="VarChar(100)")]
		public string AddressDetails2
		{
			get
			{
				return this._AddressDetails2;
			}
			set
			{
				if ((this._AddressDetails2 != value))
				{
					this.OnAddressDetails2Changing(value);
					this.SendPropertyChanging();
					this._AddressDetails2 = value;
					this.SendPropertyChanged("AddressDetails2");
					this.OnAddressDetails2Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AddressDetails3", DbType="VarChar(100)")]
		public string AddressDetails3
		{
			get
			{
				return this._AddressDetails3;
			}
			set
			{
				if ((this._AddressDetails3 != value))
				{
					this.OnAddressDetails3Changing(value);
					this.SendPropertyChanging();
					this._AddressDetails3 = value;
					this.SendPropertyChanged("AddressDetails3");
					this.OnAddressDetails3Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AddressDetails4", DbType="VarChar(100)")]
		public string AddressDetails4
		{
			get
			{
				return this._AddressDetails4;
			}
			set
			{
				if ((this._AddressDetails4 != value))
				{
					this.OnAddressDetails4Changing(value);
					this.SendPropertyChanging();
					this._AddressDetails4 = value;
					this.SendPropertyChanged("AddressDetails4");
					this.OnAddressDetails4Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PostCode", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
		public string PostCode
		{
			get
			{
				return this._PostCode;
			}
			set
			{
				if ((this._PostCode != value))
				{
					this.OnPostCodeChanging(value);
					this.SendPropertyChanging();
					this._PostCode = value;
					this.SendPropertyChanged("PostCode");
					this.OnPostCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateOfBirth", DbType="DateTime NOT NULL")]
		public System.DateTime DateOfBirth
		{
			get
			{
				return this._DateOfBirth;
			}
			set
			{
				if ((this._DateOfBirth != value))
				{
					this.OnDateOfBirthChanging(value);
					this.SendPropertyChanging();
					this._DateOfBirth = value;
					this.SendPropertyChanged("DateOfBirth");
					this.OnDateOfBirthChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastUpdatedDate", DbType="DateTime NOT NULL")]
		public System.DateTime LastUpdatedDate
		{
			get
			{
				return this._LastUpdatedDate;
			}
			set
			{
				if ((this._LastUpdatedDate != value))
				{
					this.OnLastUpdatedDateChanging(value);
					this.SendPropertyChanging();
					this._LastUpdatedDate = value;
					this.SendPropertyChanged("LastUpdatedDate");
					this.OnLastUpdatedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedDate", DbType="DateTime NOT NULL")]
		public System.DateTime CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				if ((this._CreatedDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="experian.Factors")]
	public partial class FactorEntity : DbEntity<FactorEntity>, INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _FactorId;
		
		private string _Name;
		
		private int _TypeId;
		
		private EntitySet<ParsedDataEntity> _ParsedDatas;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnFactorIdChanging(int value);
    partial void OnFactorIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnTypeIdChanging(int value);
    partial void OnTypeIdChanged();
    #endregion
		
		public FactorEntity()
		{
			this._ParsedDatas = new EntitySet<ParsedDataEntity>(new Action<ParsedDataEntity>(this.attach_ParsedDatas), new Action<ParsedDataEntity>(this.detach_ParsedDatas));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FactorId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int FactorId
		{
			get
			{
				return this._FactorId;
			}
			set
			{
				if ((this._FactorId != value))
				{
					this.OnFactorIdChanging(value);
					this.SendPropertyChanging();
					this._FactorId = value;
					this.SendPropertyChanged("FactorId");
					this.OnFactorIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TypeId", DbType="Int NOT NULL")]
		public int TypeId
		{
			get
			{
				return this._TypeId;
			}
			set
			{
				if ((this._TypeId != value))
				{
					this.OnTypeIdChanging(value);
					this.SendPropertyChanging();
					this._TypeId = value;
					this.SendPropertyChanged("TypeId");
					this.OnTypeIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="FK_Experian_ParsedData_Factors_FactorId", Storage="_ParsedDatas", ThisKey="FactorId", OtherKey="FactorId", DeleteRule="NO ACTION")]
		public EntitySet<ParsedDataEntity> ParsedDatas
		{
			get
			{
				return this._ParsedDatas;
			}
			set
			{
				this._ParsedDatas.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_ParsedDatas(ParsedDataEntity entity)
		{
			this.SendPropertyChanging();
			entity.FactorEntity = this;
		}
		
		private void detach_ParsedDatas(ParsedDataEntity entity)
		{
			this.SendPropertyChanging();
			entity.FactorEntity = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.MSSQLDeploy")]
	public partial class MSSQLDeploy : DbEntity<MSSQLDeploy>, INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private System.DateTime _Date;
		
		private string _Name;
		
		private string _MD5;
		
		private System.Nullable<int> _Revision;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnMD5Changing(string value);
    partial void OnMD5Changed();
    partial void OnRevisionChanging(System.Nullable<int> value);
    partial void OnRevisionChanged();
    #endregion
		
		public MSSQLDeploy()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MD5", DbType="VarChar(32) NOT NULL", CanBeNull=false)]
		public string MD5
		{
			get
			{
				return this._MD5;
			}
			set
			{
				if ((this._MD5 != value))
				{
					this.OnMD5Changing(value);
					this.SendPropertyChanging();
					this._MD5 = value;
					this.SendPropertyChanged("MD5");
					this.OnMD5Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Revision", DbType="Int")]
		public System.Nullable<int> Revision
		{
			get
			{
				return this._Revision;
			}
			set
			{
				if ((this._Revision != value))
				{
					this.OnRevisionChanging(value);
					this.SendPropertyChanging();
					this._Revision = value;
					this.SendPropertyChanged("Revision");
					this.OnRevisionChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="experian.Operations")]
	public partial class OperationEntity : DbEntity<OperationEntity>, INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _OperationId;
		
		private System.DateTime _Date;
		
		private int _TypeId;
		
		private int _StatusId;
		
		private string _Description;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnOperationIdChanging(int value);
    partial void OnOperationIdChanged();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnTypeIdChanging(int value);
    partial void OnTypeIdChanged();
    partial void OnStatusIdChanging(int value);
    partial void OnStatusIdChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    #endregion
		
		public OperationEntity()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OperationId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int OperationId
		{
			get
			{
				return this._OperationId;
			}
			set
			{
				if ((this._OperationId != value))
				{
					this.OnOperationIdChanging(value);
					this.SendPropertyChanging();
					this._OperationId = value;
					this.SendPropertyChanged("OperationId");
					this.OnOperationIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TypeId", DbType="Int NOT NULL")]
		public int TypeId
		{
			get
			{
				return this._TypeId;
			}
			set
			{
				if ((this._TypeId != value))
				{
					this.OnTypeIdChanging(value);
					this.SendPropertyChanging();
					this._TypeId = value;
					this.SendPropertyChanged("TypeId");
					this.OnTypeIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StatusId", DbType="Int NOT NULL")]
		public int StatusId
		{
			get
			{
				return this._StatusId;
			}
			set
			{
				if ((this._StatusId != value))
				{
					this.OnStatusIdChanging(value);
					this.SendPropertyChanging();
					this._StatusId = value;
					this.SendPropertyChanged("StatusId");
					this.OnStatusIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="VarChar(MAX)", UpdateCheck=UpdateCheck.Never)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="experian.ParsedData")]
	public partial class ParsedDataEntity : DbEntity<ParsedDataEntity>, INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ParsedDataId;
		
		private int _FactorId;
		
		private string _Value;
		
		private System.Guid _AccountId;
		
		private EntityRef<FactorEntity> _FactorEntity;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnParsedDataIdChanging(int value);
    partial void OnParsedDataIdChanged();
    partial void OnFactorIdChanging(int value);
    partial void OnFactorIdChanged();
    partial void OnValueChanging(string value);
    partial void OnValueChanged();
    partial void OnAccountIdChanging(System.Guid value);
    partial void OnAccountIdChanged();
    #endregion
		
		public ParsedDataEntity()
		{
			this._FactorEntity = default(EntityRef<FactorEntity>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParsedDataId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ParsedDataId
		{
			get
			{
				return this._ParsedDataId;
			}
			set
			{
				if ((this._ParsedDataId != value))
				{
					this.OnParsedDataIdChanging(value);
					this.SendPropertyChanging();
					this._ParsedDataId = value;
					this.SendPropertyChanged("ParsedDataId");
					this.OnParsedDataIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FactorId", DbType="Int NOT NULL")]
		public int FactorId
		{
			get
			{
				return this._FactorId;
			}
			set
			{
				if ((this._FactorId != value))
				{
					if (this._FactorEntity.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnFactorIdChanging(value);
					this.SendPropertyChanging();
					this._FactorId = value;
					this.SendPropertyChanged("FactorId");
					this.OnFactorIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Value", DbType="VarChar(255)")]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if ((this._Value != value))
				{
					this.OnValueChanging(value);
					this.SendPropertyChanging();
					this._Value = value;
					this.SendPropertyChanged("Value");
					this.OnValueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccountId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				if ((this._AccountId != value))
				{
					this.OnAccountIdChanging(value);
					this.SendPropertyChanging();
					this._AccountId = value;
					this.SendPropertyChanged("AccountId");
					this.OnAccountIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="FK_Experian_ParsedData_Factors_FactorId", Storage="_FactorEntity", ThisKey="FactorId", OtherKey="FactorId", IsForeignKey=true)]
		public FactorEntity FactorEntity
		{
			get
			{
				return this._FactorEntity.Entity;
			}
			set
			{
				FactorEntity previousValue = this._FactorEntity.Entity;
				if (((previousValue != value) 
							|| (this._FactorEntity.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._FactorEntity.Entity = null;
						previousValue.ParsedDatas.Remove(this);
					}
					this._FactorEntity.Entity = value;
					if ((value != null))
					{
						value.ParsedDatas.Add(this);
						this._FactorId = value.FactorId;
					}
					else
					{
						this._FactorId = default(int);
					}
					this.SendPropertyChanged("FactorEntity");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="experian.ParsedDataTemp")]
	public partial class ParsedDataTempEntity : DbEntity<ParsedDataTempEntity>, INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ParsedDataTempId;
		
		private int _FactorId;
		
		private string _Value;
		
		private int _CustomerId;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnParsedDataTempIdChanging(int value);
    partial void OnParsedDataTempIdChanged();
    partial void OnFactorIdChanging(int value);
    partial void OnFactorIdChanged();
    partial void OnValueChanging(string value);
    partial void OnValueChanged();
    partial void OnCustomerIdChanging(int value);
    partial void OnCustomerIdChanged();
    #endregion
		
		public ParsedDataTempEntity()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParsedDataTempId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ParsedDataTempId
		{
			get
			{
				return this._ParsedDataTempId;
			}
			set
			{
				if ((this._ParsedDataTempId != value))
				{
					this.OnParsedDataTempIdChanging(value);
					this.SendPropertyChanging();
					this._ParsedDataTempId = value;
					this.SendPropertyChanged("ParsedDataTempId");
					this.OnParsedDataTempIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FactorId", DbType="Int NOT NULL")]
		public int FactorId
		{
			get
			{
				return this._FactorId;
			}
			set
			{
				if ((this._FactorId != value))
				{
					this.OnFactorIdChanging(value);
					this.SendPropertyChanging();
					this._FactorId = value;
					this.SendPropertyChanged("FactorId");
					this.OnFactorIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Value", DbType="VarChar(255)")]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if ((this._Value != value))
				{
					this.OnValueChanging(value);
					this.SendPropertyChanging();
					this._Value = value;
					this.SendPropertyChanged("Value");
					this.OnValueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerId", DbType="Int NOT NULL")]
		public int CustomerId
		{
			get
			{
				return this._CustomerId;
			}
			set
			{
				if ((this._CustomerId != value))
				{
					this.OnCustomerIdChanging(value);
					this.SendPropertyChanging();
					this._CustomerId = value;
					this.SendPropertyChanged("CustomerId");
					this.OnCustomerIdChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="experian.Types")]
	public partial class TypeEntity : DbEntity<TypeEntity>, INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _TypeId;
		
		private string _Name;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnTypeIdChanging(int value);
    partial void OnTypeIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public TypeEntity()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TypeId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int TypeId
		{
			get
			{
				return this._TypeId;
			}
			set
			{
				if ((this._TypeId != value))
				{
					this.OnTypeIdChanging(value);
					this.SendPropertyChanging();
					this._TypeId = value;
					this.SendPropertyChanged("TypeId");
					this.OnTypeIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
