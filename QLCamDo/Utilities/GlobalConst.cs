using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCamDo.Utilities
{
    public class GlobalConst
    {
        #region Global

        public static string GetConnecStringName
        {
            get
            {
                string connection = "NLOPCEntities" + (DateTime.Now.Year.ToString() == "2016" ? string.Empty : DateTime.Now.Year.ToString());
                return connection;
            }
        }

        public static int PageSize = 30;

        #endregion

        #region Session

        public static string SessionMessage = "SESSION_MESSAGE";
        public static string OAuthSession = "SESSION_OAUTH";    
        public static string ReturnUrlSession = "SESSION_RETURN_URL";    

        #endregion   
      
    }
}
