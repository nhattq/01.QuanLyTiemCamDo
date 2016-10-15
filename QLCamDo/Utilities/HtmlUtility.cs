using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCamDo.Utilities
{
    public class HtmlUtility
    {
        public enum ActionType
        {
            Created = 1,
            Updated = 2,
            Deleted = 3,
            Imported = 4,
            ImporteFail = 5,
        }
        public enum MessageType
        {
            Error = 1,
            Success = 0,
            Warning = 2,
        }
        public static string BuildHtmlActive(bool status)
        {
            return status ? "<span class=\"label label-success\"><i class=\"fa fa-check-circle-o\"></i></span>" : "<span class=\"label label-danger\"><i class=\"fa fa-ban\"></i></span>";
        }
        public static string BuildHtmlContractStatus(byte status)
        {
            return status == 0 ? "<span class=\"label label-success\">Đã thanh lý</span>" : "<span class=\"label label-danger\">Đang hiệu lực</span>";
        }
        public static string BuildHtmlAccess(bool status)
        {
            return status ? "<span class=\"label label-success\">Được phép</span>" : "<span class=\"label label-danger\">Không được phép</span>";
        }

        public static string BuildMessageTemplate(string content, MessageType type)
        {
            string html = "<div class='alert alert-{0} fade in'>" +
                                 "<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" +
                                 "<i class='fa {1}'></i>" +
                                 "<strong>{2}</strong> {3}</a>." +
                            "</div>";
            switch ((int)type)
            {
                case 1:
                    return string.Format(html, "danger", "fa-times-circle fa-fw fa-lg", "Opps", content);
                    break;
                case 0:
                    return string.Format(html, "success", "fa-check-circle fa-fw fa-lg", "Thành công", content);
                    break;
                case 2:
                    return string.Format(html, "warning", "fa-warning fa-fw fa-lg", "Cảnh báo", content);
                    break;
            }
            return string.Empty;
        }

        public static string BuildPaging(int currentPage, int totalPage, string controller, string action)
        {
            if (totalPage == 1)
                return string.Empty;

            string link = "/" + controller + "/" + action + "?page=";
            string html = string.Empty;
            html += "<ul class='pagination no-margin'>";
            html += "<li><a href='" + link + ((currentPage - 1) <= 0 ? 1 : currentPage) + "' aria-label='Previous'><span class='style-padding' aria-hidden='true'>&laquo;</span></a></li>";

            for (int i = 1; i <= totalPage; i++)
            {
                html += "<li class=" + ((currentPage == 0 & i == 1 || i == currentPage + 1) ? "'active'" : "''") + "><a href='" + (link + i) + "'>" + i + "<span class='sr-only'>(current)</span></a></li>";
            }

            html += "<li><a href='" + (link + ((currentPage + 1) >= totalPage ? totalPage : (currentPage + 2))) + "' aria-label='Next'><span class='style-padding' aria-hidden='true'>&raquo;</span></a></li>";
            html += "</ul>";
            return html;
        }
    }
}
