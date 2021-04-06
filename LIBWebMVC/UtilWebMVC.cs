using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using LIBUtil;

namespace LIBWebMVC
{
    public class UtilWebMVC
    {
        /**********************************************************************************************************************************************************/
        #region WEB APP

        public static void debug(ModelStateDictionary ModelState, ViewDataDictionary ViewData)
        {
            if (!ModelState.IsValid)
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        string errorMessage = error.ErrorMessage;
                        string exceptionMessage = error.Exception.Message;
                    }
                }
            }
        }

        public static string getImageSource(object image, string filename)
        {
            if (image == null || image == DBNull.Value)
                return string.Empty;
            else if (!string.IsNullOrEmpty(filename))
                return string.Format("data:image/{1};base64,{0}", Convert.ToBase64String((Byte[])image), filename.Substring(filename.LastIndexOf('.')));
            else
                return string.Format("data:image/*;base64,{0}", Convert.ToBase64String((Byte[])image));
        }

        public static string getApplicationPath(HttpRequestBase Request, string filepath)
        {
            return Request.ApplicationPath + filepath;
        }

        public static bool hasAccess(HttpSessionStateBase Session, string key)
        {
            return Session[key].ToString().ToLower() == "true";
        }

        public static string getSessionString(HttpSessionStateBase Session, string key) { return getSessionObject(Session, key).ToString(); }
        public static object getSessionObject(HttpSessionStateBase Session, string key)
        {
            return Session[key];
        }

        public static SelectList getSelectList(DataTable datatable, string dataValueField, string dataTextField)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //go through the sex model and populate you selectlist items
            foreach (DataRow row in datatable.Rows)
            {
                items.Add(new SelectListItem()
                {
                    Value = row[dataValueField].ToString(),
                    Text = row[dataTextField].ToString()
                });
            }
            return new SelectList(items, "Value", "Text");
        }

        public static object validateParameter(object value)
        {
            if (value != null && (value.ToString() == "undefined" || value.ToString() == "NaN"))
                return "";
            else
                return value;
        }

        public static bool hasBootboxMessage(ControllerBase controller)
        {
            return !string.IsNullOrEmpty(controller.ViewBag.BootboxMessage);
        }

        public static void setBootboxMessage(ControllerBase controller, string message)
        {
            controller.TempData["BootboxMessage"] = message.Replace(Environment.NewLine, "<BR>");
        }

        #endregion
        /**********************************************************************************************************************************************************/
        #region ENUMS

        public static SelectList GetEnumSelectList<T>()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (SelectListItem item in System.Web.Mvc.Html.EnumHelper.GetSelectList(typeof(T)))
                list.Add(new SelectListItem() { Value = item.Value, Text = Util.GetEnumDescription<T>(item.Value) });

            return new SelectList(list, "Value", "Text");
        }

        #endregion
        /**********************************************************************************************************************************************************/
    }
}
