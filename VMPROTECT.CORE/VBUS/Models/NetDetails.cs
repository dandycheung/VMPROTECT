using VMPROTECT.CORE.VBUS.Exceptions;

namespace VMPROTECT.CORE.VBUS.Models
{
    public class NetDetails
    {
        private int _port = 9313;
        private string _ipAddr4 = "127.0.0.1";
        public int Port
        {
            get => _port;
            set => _port = value;
        }
        public string IpAddr4
        {
            get => _ipAddr4;
            set
            {
                string[] splitValues = value.Split('.');
                if(splitValues.Length != 4)
                {
                    throw new InvalidIpAddress(value);
                }
                byte tempForParsing;
                if(!splitValues.All(r => byte.TryParse(r, out tempForParsing)))
                {
                    throw new InvalidIpAddress(value);
                }
                _ipAddr4 = value;
            }
        }
    }
}
