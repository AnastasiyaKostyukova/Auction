﻿function viewLots() {
    var self = this;

    self.currentTab = "";
    self.lastSearchQueryString = "";

    self.openTab = function (tabName) {
        if (self.currentTab == tabName) {
            return;
        }

        // clear last search
        self.lastSearchQueryString = "";
        self.clearFilter();

        var url = self.LOT_ACTION_URL;
        url += buildQueryString(tabName);

        loadNewData(url);

        self.lightTab(tabName);
        self.currentTab = tabName;
    };

    self.searchByParameters = function() {
        var url = self.LOT_ACTION_URL;
        url += buildQueryString(self.currentTab, 1, true);
        loadNewData(url);
    };

    self.openPage = function(pageNumber) {
        var url = self.LOT_ACTION_URL;
        url += buildQueryString(self.currentTab, pageNumber);
        url += self.lastSearchQueryString;

        loadNewData(url);
        $("html, body").animate({ scrollTop: 0 }, "slow");
    };

    self.renderPageButtons = function (currentPageNumber, maxPage) {
        var firstPageButton = "#goToFirstPage";
        var page1Button = "#goToPage1";
        var page2Button = "#goToPage2";
        var page3Button = "#goToPage3";
        var lastPageButton = "#goToLastPage";

        changeVisibilityPageButtons([firstPageButton, page1Button, page2Button, page3Button, lastPageButton], true);

        if (maxPage < 4) {
            var shouldBeLighted = "#goToPage" + currentPageNumber;

            switch (maxPage) {
                case 2:
                    displayNumericButtonsOfPage(2, shouldBeLighted, 1);
                    break;
                case 3:
                    displayNumericButtonsOfPage(3, shouldBeLighted, 1);
            }
        } else {
            if (currentPageNumber == 1) {
                displayNumericButtonsOfPage(3, page1Button, currentPageNumber);
                changeVisibilityPageButtons([lastPageButton]);
            } else if (currentPageNumber == maxPage) {
                displayNumericButtonsOfPage(3, page3Button, currentPageNumber - 2);
                changeVisibilityPageButtons([firstPageButton]);
            } else {
                displayNumericButtonsOfPage(3, page2Button, currentPageNumber - 1);
                changeVisibilityPageButtons([firstPageButton, lastPageButton]);
            }
        }
    };

    self.numericButtonsOfPagePressed = function(buttonId) {
        var text = $('#' + buttonId).text();

        if (text) {
            self.openPage(text);
        }
    };

    self.clearFilter = function() {
        $('#authorArtworkName').val('');
        $('#artworkName').val('');
        $('#minimalPrice').val('');
        $('#maximalPrice').val('');
        document.getElementById("oderbyDate").checked = false;
        $('#countLotsOnPage').val('5');
    }

    self.lightTab = function(tabName) {
        $('#allLotsButton').removeClass('btn-danger');
        $('#myLotsButton').removeClass('btn-danger');
        $('#myRatesLotsButton').removeClass('btn-danger');
        $('#myWinsLotsButton').removeClass('btn-danger');

        if (tabName == self.MY_LOTS) {
            $('#myLotsButton').addClass('btn-danger');
        } else if (tabName == self.ALL_LOTS) {
            $('#allLotsButton').addClass('btn-danger');
        } else if (tabName == self.MY_WINS_LOTS) {
            $('#myWinsLotsButton').addClass('btn-danger');
        } else {
            $('#myRatesLotsButton').addClass('btn-danger');
        }
    };

    function loadNewData(url) {
        console.log(url);
        $('#lotList').html(buildLoadingImageTag());
        $('#lotList').load(url);
    }

    function buildLoadingImageTag() {
        return "<div class='center-align'><img src='" + self.loadingImage + "'/></div>";
    }

    function buildQueryString(tabName, pageNumber, withSearch) {
        var queryString = "?";
        queryString += "tab=" + tabName;

        if (pageNumber) {
            queryString += "&PageNumber=" + pageNumber;
        }

        if (!withSearch) {
            return queryString;
        }

        var searchQueryString = '';
        var countLotsOnPage = $('#countLotsOnPage').val();
        if (countLotsOnPage) {
            searchQueryString += "&LotsCountOnPage=" + countLotsOnPage;
        }

        var lotauthName = $('#authorArtworkName').val();
        if (lotauthName) {
            searchQueryString += "&PictureAuthor=" + lotauthName;
        }

        var lotName = $('#artworkName').val();
        if (lotName) {
            searchQueryString += "&ArtworkName=" + lotName;
        }

        var minPrice = $('#minimalPrice').val();
        if (+minPrice) {
            searchQueryString += "&MinPrice=" + minPrice;
        } else {
            $('#minimalPrice').val('');
        }

        var maxPrice = $('#maximalPrice').val();
        if (+maxPrice) {
            searchQueryString += "&MaxPrice=" + maxPrice;
        } else {
            $('#maximalPrice').val('');
        }

        var oderbyDate = document.getElementById("oderbyDate").checked;
        if (oderbyDate) {
            searchQueryString += "&OrderByAuctionDate=" + oderbyDate;
        }

        self.lastSearchQueryString = searchQueryString;
        queryString += searchQueryString;
        return queryString;
    }

    function changeVisibilityPageButtons(buttonsId, isHide) {
        for (var i = 0; i < buttonsId.length; i++) {
            if (isHide) {
                $(buttonsId[i]).addClass('hidden');
            } else {
                $(buttonsId[i]).removeClass('hidden');
            }
        }
    }

    function displayNumericButtonsOfPage(numberOfButton, currentPageButtonId, startTextNumber) {
        var idButton = "#goToPage";

        for (var i = 1; i <= numberOfButton; i++) {
            $(idButton + i).removeClass('hidden');
            $(idButton + i).text(startTextNumber + i - 1);

            if ((idButton + i) != currentPageButtonId) {
                $(idButton + i).removeClass('btn-success');
            } else {
                $(idButton + i).addClass('btn-success');
            }
        }
    }
}