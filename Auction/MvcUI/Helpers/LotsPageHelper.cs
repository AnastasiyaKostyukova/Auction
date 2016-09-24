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
            var links = BuildPageLinks(lotsViewModel, lotsPageUrl, updateTagId, onclickFunction);
            return MvcHtmlString.Create(links);
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

        private static string BuildPageLinks(LotsViewModel lotsViewModel,
            Func<int, string> lotsPageUrl, string updateTagId,
            string onclickFunction)
        {
            if (lotsViewModel.MaxPageNumber == 1)
            {
                return string.Empty;
            }

            var result = new StringBuilder();
            var startIndex = 1;
            var numberOfButtons = lotsViewModel.MaxPageNumber >= 3
                ? 3 
                : lotsViewModel.MaxPageNumber;

            var showLastButton = true;

            if (lotsViewModel.MaxPageNumber < 4)
            {
                startIndex = 1;
                showLastButton = false;
            }
            else
            {
                if (lotsViewModel.MaxPageNumber == lotsViewModel.PageNumber)
                {
                    startIndex = lotsViewModel.PageNumber - 2;
                    showLastButton = false;
                }
                else if (lotsViewModel.PageNumber == 1)
                {
                    startIndex = 1;
                }
                else
                {
                    startIndex = lotsViewModel.PageNumber - 1;
                }
            }

            if (startIndex != 1)
            {
                var firstLink = BuildPageLink(lotsPageUrl, updateTagId, onclickFunction, 1, "First", false);
                result.Append(firstLink);
            }

            for (var i = startIndex; i <= startIndex + numberOfButtons - 1; i++)
            {
                var link = BuildPageLink(lotsPageUrl, updateTagId, onclickFunction, i, i.ToString(),
                    i == lotsViewModel.PageNumber);

                result.Append(link);
            }

            if (showLastButton)
            {
                var lastLink = BuildPageLink(lotsPageUrl, updateTagId, onclickFunction, lotsViewModel.MaxPageNumber, "Last", false);
                result.Append(lastLink);
            }

            return result.ToString();
        }

        private static string BuildPageLink(
            Func<int, string> lotsPageUrl,
            string updateTagId, 
            string onclickFunction,
            int numberOfPage,
            string linkText, 
            bool toLight )
        {
            var tag = new TagBuilder("a");
            tag.MergeAttribute("href", lotsPageUrl(numberOfPage));
            tag.InnerHtml = linkText;

            tag.MergeAttribute("data-ajax", "true");
            tag.MergeAttribute("data-ajax-method", "GET");
            tag.MergeAttribute("data-ajax-mode", "replace");
            tag.MergeAttribute("data-ajax-update", "#" + updateTagId);

            if (string.IsNullOrEmpty(onclickFunction) == false)
            {
                tag.MergeAttribute("onclick", onclickFunction);
            }
            
            tag.AddCssClass("btn");

            if (toLight)
            {
                tag.AddCssClass("btn-success");
            }

            return tag.ToString();
        }
    }
}