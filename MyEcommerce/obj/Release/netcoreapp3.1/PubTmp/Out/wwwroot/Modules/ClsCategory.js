let myitems = [];
let ClsCategory =
{
    LoadCategories: function () {
        Helper.AjaxCallGet("/api/CategoryApi", {}, "json", function (data) {
            let htmlData = "";
            let htmlData2 = "";

            console.log("------------------------------------------------------------");
            console.log(data);
            console.log("------------------------------------------------------------");
           
         
            for (let i = 0; i < data.length; i++) {
                htmlData = htmlData + ClsCategory.Template1(data[i]);


   
            }
            $("#accordionExample").html(htmlData);
          


            // filteration by price
            
           
            


           

            htmlData2 = htmlData2 + ClsCategory.Template2();
          
            $("#myfilter").html(htmlData2);
           
            var rangeSlider = $(".price-range"),
                minamount = $("#minamount"),
                maxamount = $("#maxamount"),
                minPrice = rangeSlider.data('min'),
                maxPrice = rangeSlider.data('max');
            rangeSlider.slider({
                range: true,
                min: minPrice,
                max: maxPrice,
                values: [minPrice, maxPrice],
                slide: function (event, ui) {
                    minamount.val( ui.values[0]);
                    maxamount.val( ui.values[1]);
                    p1 = document.getElementById("minamount").value;
                    p2 = document.getElementById("maxamount").value;

                    ClsItems2.filtermyitems2(p1, p2);
                    
                    
                }

            });
            minamount.val('$' + rangeSlider.slider("values", 0));
            maxamount.val('$' + rangeSlider.slider("values", 1));

            
     
           // document.getElementById("afilter").addEventListener("click", ClsItems2.filtermyitems2(p1, p2));
         // document.getElementById("afilter").addEventListener("click", ClsItems2.filtermyitems2(document.getElementById("minamount").value, document.getElementById("maxamount").value));


           
            //let r1 = document.getElementById('minamount').value;
            //let r2 = document.getElementById('maxamount').value;
            //console.log("r1 = " + r1);
            //console.log("r2 = " + r2);

        },
            function () {

            });
    },
    Template1: function (cat) {
        //let cattegory = " <li><a onclick='ClsItems.FilterItems(" + cat.categoryId + ")'>" + cat.categoryName + " <span>(3)</span></a></li>";
    
        
        let cattegory = "<div class='card'>";
        cattegory = cattegory + "<div class='card-heading active'>";
        cattegory = cattegory + "<a      data-toggle='collapse' data-target='#" + cat.categoryName + "'   onclick='ClsItems2.filtermyitems(" + cat.categoryId+")'   >" + cat.categoryName+"</a>";
        cattegory = cattegory + "</div>";
        cattegory = cattegory + "<div id='" + cat.categoryName+"' class='collapse' data-parent='#accordionExample'>";
        cattegory = cattegory + "<div class='card-body'>";
        cattegory = cattegory + "<ul>";
        cattegory = cattegory + "<li><a href='#'>Coats</a></li>";
        cattegory = cattegory + "<li><a href='#'>Coats</a></li>";
        cattegory = cattegory + "<li><a href='#'>Coats</a></li>";
        cattegory = cattegory + "<li><a href='#'>Coats</a></li>";
        cattegory = cattegory + "<li><a href='#'>Coats</a></li>";
        cattegory = cattegory + "<li><a href='#'>Coats</a></li>";
        cattegory = cattegory + "</ul>";
        cattegory = cattegory + "</div>";
        cattegory = cattegory + "</div>";
        cattegory = cattegory + "</div>";
      

        return cattegory;
    },
    Template2: function () {
        //let cattegory = " <li><a onclick='ClsItems.FilterItems(" + cat.categoryId + ")'>" + cat.categoryName + " <span>(3)</span></a></li>";

       

        let category2 = "<div class='section-title'>";
        category2 = category2 + "<h4>Shop by price</h4>";
        category2 = category2 + "</div>";
        category2 = category2 + "<div class='filter-range-wrap'>";
        category2 = category2 + "<div class='price-range ui-slider ui-corner-all ui-slider-horizontal ui-widget ui-widget-content' data-min='3000' data-max='5000' id='toto'></div>";
        category2 = category2 + "<div class='range-slider'>";
        category2 = category2 + "<div class='price-input'>";
        category2 = category2 + "<p>Price:</p>";
        category2 = category2 + "<input type='text'   id='minamount'>";
        category2 = category2 + "<input type='text' id='maxamount'>";
        category2 = category2 + "</div>";
        category2 = category2 + "</div>";
        category2 = category2 + "</div>";
        category2 = category2 + "<a  id='afilter' onclick='ClsItems2.filtermyitems2(2000,3000);' >Filter</a>";
       


        return category2;
    }
}
ClsCategory.LoadCategories();

