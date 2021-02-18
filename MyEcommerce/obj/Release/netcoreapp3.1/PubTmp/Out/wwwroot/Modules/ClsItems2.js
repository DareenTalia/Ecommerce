
let Items = [];
let ClsItems2 = {
    LoadItems: function () {
        Helper.AjaxCallGet("/api/ItemApi", {}, "json", function (data) {
          
            Items = data;
            //console.log(data);
            let htmlData = "";
            for (let i = 0; i < data.length; i++) {
            htmlData = htmlData + ClsItems2.Template1(data[i]);
              
               // ClsItems2.loadImage(data[i]);

            }

           
            $("#DivTemplate1").html(htmlData);

            for (let i = 0; i < data.length; i++) {
                $('.set-bg').each(function () {
                    var bg = $(this).data('setbg');
                    $(this).css('background-image', 'url(' + bg + ')');
                    console.log($(this).data('setbg'));

                });

            }
           //console.log(htmlData);
            // ClsItems2.loadImage();
         
        },
            function () {

            });
    },
    Template1: function (item) {
        //ClsItems2.loadImage();
       
        let itemHtml = "<div class='col-lg-4 col-md-6'>";
        itemHtml = itemHtml + "<div class='product__item'>";
        itemHtml = itemHtml + "<div class='product__item__pic set-bg' id='ty'  data-setbg='/Uploads/" + item.imageName + "'>";
        itemHtml = itemHtml + "<div class'label new'>New</div>";
        itemHtml = itemHtml + "<ul class='product__hover'>";
        itemHtml = itemHtml + "<li><a href='/Uploads/" + item.imageName + "' class='image-popup'><span class='arrow_expand'></span></a></li>";
        itemHtml = itemHtml + "<li><a href='#'><span class='icon_heart_alt'></span></a></li>";
        itemHtml = itemHtml + "<li><a href='#'><span class='icon_bag_alt'></span></a></li>";
        itemHtml = itemHtml + "</ul>";
        itemHtml = itemHtml + "</div>";
        itemHtml = itemHtml + "<div class='product__item__text'>";
        itemHtml = itemHtml + "<h6><a href='#'>"+item.itemName+"</a></h6>";
        itemHtml = itemHtml + "<div class='rating'>";
        itemHtml = itemHtml + "<i class='fa fa-star'></i>";
        itemHtml = itemHtml + "<i class='fa fa-star'></i>";
        itemHtml = itemHtml + "<i class='fa fa-star'></i>";
        itemHtml = itemHtml + "<i class='fa fa-star'></i>";
        itemHtml = itemHtml + "<i class='fa fa-star'></i>";
        itemHtml = itemHtml + "</div>";
        itemHtml = itemHtml + "<div class='product__price'>"+item.salesPrice+"</div>";
        itemHtml = itemHtml + "</div>";
        itemHtml = itemHtml + "</div>";
        itemHtml = itemHtml + "</div>";


        //ClsItems2.loadImage();
       
        return itemHtml;
    }
    , 
   filtermyitems:function (catId) {
       //console.log(catId);
       let newItems = $.grep(Items, function (n, i) { // just use arr
           return n.categoryId === catId;
       });
       //console.log(newItems);
       let htmlData = "";
       for (let i = 0; i < newItems.length; i++) {
           htmlData = htmlData + ClsItems2.Template1(newItems[i]);
       }
       $("#DivTemplate1").html(htmlData);

       for (let i = 0; i < newItems.length; i++) {
           $('.set-bg').each(function () {
               var bg = $(this).data('setbg');
               $(this).css('background-image', 'url(' + bg + ')');

           });

       }
   
    }
    ,
    filtermyitems2: function (p1, p2) {

        
        //console.log("cls items  minprice =  " + p1, " -- maxprice =  " + p2);
        let newItems = $.grep(Items, function (n, i) { // just use arr
            return n.salesPrice > p1 && n.salesPrice < p2 ;
        });
        //console.log(newItems);
        let htmlData = "";
        for (let i = 0; i < newItems.length; i++) {
            htmlData = htmlData + ClsItems2.Template1(newItems[i]);
        }
        $("#DivTemplate1").html(htmlData);

        for (let i = 0; i < newItems.length; i++) {
            $('.set-bg').each(function () {
                var bg = $(this).data('setbg');
                $(this).css('background-image', 'url(' + bg + ')');

            });

        }

    }
   

}

ClsItems2.LoadItems();



