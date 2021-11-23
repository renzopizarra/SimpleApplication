(function () {
    window.onload = _fnGetAllTerritories();


    function _fnGetAllTerritories() {
        $.ajax({
            url: "/Home/GetTerritories",
            data: {},
            type: 'GET',
            //async: false,
            success: function (result) {
                var oData = JSON.parse(result);
                var url = "#";
                var ul = document.createElement('ul');
                var mainUl = $('#myUL');
                var fTerritory = "";
                var sTerritory = "";
                var tTerritory = "";

                var groupBy = function (xs, key) {
                    return xs.reduce(function (rv, x) {
                        (rv[x[key]] = rv[x[key]] || []).push(x);
                        return rv;
                    }, {});
                };
                var groubedByTeam = groupBy(oData.data, 'parent');

                oData.data.sort(function (a, b) {
                    return a.parent - b.parent;
                });

                function recurseMenu(parent) {
                    var s = '';
                    if (s.indexOf("myUL") >= 0) {
                        s = '<ul class="nested">';
                    } else {
                        s = '<ul id="myUL">';
                    }
                    for (var x in oData.data) {
                        if (oData.data[x].parent == parent) {
                            s += '<li><span class="caret">' + oData.data[x].name + '</span>';

                            if (oData.data[x].parent != null || oData.data[x].parent != "null") {
                                s += recurseMenu(oData.data[x].id);
                            }
                            s += '</li>';
                        }
                    }
                    return s + '</ul>';
                }

                $(".pb-3").html(recurseMenu());
            }
        });
    }
}) ();