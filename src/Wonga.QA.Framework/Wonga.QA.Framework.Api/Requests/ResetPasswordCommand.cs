using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api
{
    /// <summary> Wonga.Ops.Commands.ResetPassword </summary>
    [XmlRoot("ResetPassword")]
    public partial class ResetPasswordCommand : ApiRequest<ResetPasswordCommand>
    {
        public Object PwdResetKey { get; set; }
        public Object NewPassword { get; set; }
    }
}
