using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace SCv20.Tools.Web.App_Classes {
    public class SessionVariables {
        

        public int CampaignID {
            get {
                return Convert.ToString(HttpContext.Current.Session["__CampaignID"]).SafeInt32();
            }
            set {
                HttpContext.Current.Session["__CampaignID"] = value;
            }
        }

        public int MissionID {
            get {
                return Convert.ToString(HttpContext.Current.Session["__MissionID"]).SafeInt32();
            }
            set {
                HttpContext.Current.Session["__MissionID"] = value;
            }
        }
    }
}