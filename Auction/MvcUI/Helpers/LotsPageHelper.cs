using System;
using System.Text;
using System.Web.Mvc;
using MvcUI.ViewModels;

namespace MvcUI.Helpers
{
    public static class LotsPageHelper
    {
        public static MvcHtmlString LotsPageLinks(
            this HtmlHelper html,
            LotsViewModel lotsViewModel,
            string updateTagId,
            Func<int, string> lotsPageUrl,
            string onclickFunction)
        {
            var result = new StringBuilder();

            for (int i = 1; i <= lotsViewModel.MaxPageNumber; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", lotsPageUrl(i));
                tag.InnerHtml = i.ToString();

                tag.MergeAttribute("data-ajax", "true");
                tag.MergeAttribute("data-ajax-method", "GET");
                tag.MergeAttribute("data-ajax-mode", "replace");
                tag.MergeAttribute("data-ajax-update", "#" + updateTagId);

                if (string.IsNullOrEmpty(onclickFunction) == false)
                {
                    tag.MergeAttribute("onclick", onclickFunction);
                }

                if (i == lotsViewModel.PageNumber)
                {
                    tag.AddCssClass("btn-success");
                }

                result.Append(tag);
            }

            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString BuildLotsTabs(
            this HtmlHelper html,
            LotsRequestModel lotsRequestModel,
            string buttonText,
            string tabParam,
            Func<string, string> urlFunc)
        {
            var tag = new TagBuilder("a");
            tag.MergeAttribute("href", urlFunc(tabParam));
            tag.InnerHtml = buttonText;

            tag.AddCssClass("btn");
            tag.AddCssClass(lotsRequestModel.Tab == tabParam ? "btn-success" : "btn-info");

            return MvcHtmlString.Create(tag.ToString());
        }
    }
}