using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Business.Extensions
{
    public static class HelperMethods
    {
        /// <summary>
        /// Performs a null-safe .ToString() operation.
        /// </summary>
        /// <param name="testObj">The object .ToString() is being called on.</param>
        /// <returns>The string representation of the provided object, if any.</returns>
        public static string SaferToString(this object testObj)
        {
            return (testObj == null) ? String.Empty : testObj.ToString();
        }

        /// <summary>
        /// Determines which Control triggered the PostBack on a page
        /// </summary>
        /// <param name="page">The current Page object</param>
        /// <returns>The Control that triggered the PostBack</returns>
        public static Control GetPostBackControl(this Page page)
        {
            return !string.IsNullOrEmpty(page.Request.Params.Get("__EVENTTARGET")) ? page.FindControl(page.Request.Params.Get("__EVENTTARGET")) : (page.Request.Form.Cast<string>().Select(page.FindControl)).OfType<Control>().FirstOrDefault();
        }

        /// <summary>
        /// Used to easily find a nested control within Master Pages and PlaceHolders.
        /// </summary>
        /// <param name="startElement">The starting control (Page, MasterPage, etc.)</param>
        /// <param name="controlToFind">The ID of the control; same as FindControl("foo")</param>
        /// <returns>The identified Control instance, if any.</returns>
        public static Control FindDeepControl(this Control startElement, string controlToFind)
        {
            return startElement.ID == controlToFind ? startElement : (from Control c in startElement.Controls select FindDeepControl(c, controlToFind)).FirstOrDefault(found => found != null);
        }
    }
}