let Items = [];
let myarray = [];
let  filterstate = false;

let clsCart = {
    saveCart: function () {


        Helper.AjaxCallGet("/api/CategoryApi", {}, "json", function (data) {

           
            //console.log(data);
            let htmlData = " <li  id='all'   class='active' data-filter='*'  onclick='clsCart.saveCart();'  >All</li>";
            console.log("-------data categoreis start ");
            console.log(data);
            console.log("-------data categoreis end ");
           
            for (let i = 0; i < data.length; i++) {
                htmlData = htmlData + clsCart.Template2(data[i]);
            }

            console.log(htmlData);
            $("#dvPostItems2").html(htmlData);


            for (let i = 0; i < data.length; i++) {
                $('.set-bg').each(function () {
                    var bg = $(this).data('setbg');
                    $(this).css('background-image', 'url(' + bg + ')');
                    // console.log($(this).data('setbg'));

                });

            }




            // console.log(htmlData);
            // ClsItems2.loadImage();

        },
            function () {

            });

        //---------------------------- LoadData ---------------------------------------------------------

        Helper.AjaxCallGet("/api/ItemApi", {}, "json", function (data) {

            Items = data;
            //console.log(data);
            let htmlData = "";
            console.log( data);
            for (let i = 0; i < data.length; i++) {
                htmlData = htmlData + clsCart.Template1(data[i]);
            }


            $("#dvPostItems").html(htmlData);


            for (let i = 0; i < data.length; i++) {
                $('.set-bg').each(function () {
                    var bg = $(this).data('setbg');
                    $(this).css('background-image', 'url(' + bg + ')');
                    // console.log($(this).data('setbg'));

                });

            }




            // console.log(htmlData);
            // ClsItems2.loadImage();

        },
            function () {

            });
        //-------------------------------------------------------------------------------------








    },
  


    Template1: function (item) {

        // console.log("CategoryName is ---  " + item.categoryName);
        let itemHtml = "<div class='col-lg-3 col-md-4 col-sm-6 mix " + item.categoryName + "'  data-filter='." + item.categoryName + "' >";
        itemHtml = itemHtml + "<div class='product__item'>";
        itemHtml = itemHtml + "<input  type='hidden'  id='itemId'  value='" + item.itemId + "' />";
        itemHtml = itemHtml + "<div class='product__item__pic set-bg' id='imageName'  data-setbg='/Uploads/" + item.imageName + "'>";
        itemHtml = itemHtml + "<div class='label new'>New</div>";
        itemHtml = itemHtml + "<ul class='product__hover'>";
        itemHtml = itemHtml + "<li><a  href='/Uploads/" + item.imageName + "' class='image-popup'><span class='arrow_expand'></span></a></li>";
        itemHtml = itemHtml + "<li><a href='#'><span class='icon_heart_alt'></span></a></li>";
        itemHtml = itemHtml + "<li><a    onclick='clsCart.saveInCart(" + item.itemId + ");'   id='Save'  ><span class='icon_bag_alt'></span></a></li>";
        itemHtml = itemHtml + "</ul>";
        itemHtml = itemHtml + "</div>";
        itemHtml = itemHtml + "<div class='product__item__text'>";
        itemHtml = itemHtml + "<h6><a     href='/Item/Details/" + item.itemId + "' id='" + item.itemName + "' >" + item.itemName + "</a></h6>";
        itemHtml = itemHtml + " <div class='rating'>";
        itemHtml = itemHtml + "<i class='fa fa-star'></i>";
        itemHtml = itemHtml + "<i class='fa fa-star'></i>";
        itemHtml = itemHtml + "<i class='fa fa-star'></i>";
        itemHtml = itemHtml + "<i class='fa fa-star'></i>";
        itemHtml = itemHtml + "<i class='fa fa-star'></i>";
        itemHtml = itemHtml + "</div>";
        itemHtml = itemHtml + "<div class='product__price'  id='salesPrice' >" + item.salesPrice + "</div>";
        itemHtml = itemHtml + "</div>";
        itemHtml = itemHtml + "</div>";
        itemHtml = itemHtml + "</div>";



        return itemHtml;
    }

    ,

    Template2: function (cat) {

        // console.log("CategoryName is ---  " + item.categoryName);
        let itemHtml = "<li  id='" + cat.categoryId +"' class=''   onclick='clsCart.filtermyitems(" + cat.categoryId +")'   data-filter='." + cat.categoryName + "' >" + cat.categoryName + "</li>";

       
        return itemHtml;
    }
    ,

    filtermyitems: function (catId , event ) {
        //console.log(catId);
        let all = document.getElementById("all");
       
        myarray.push(catId);
        //let any = myarray[0];
        for (i = 0; i < myarray.length; i++) {

          
            document.getElementById(myarray[i]).className = "";
        }
        document.getElementById(myarray[myarray.length - 1]).className = "active";
        all.className = "";
            
        
            
        //event.preventDefault();
     





        let newItems = $.grep(Items, function (n, i) { // just use arr
            return n.categoryId === catId;
        });

        console.log("START  NEW ITEMS  -------------------------------------");
        console.log(newItems);

        console.log("END   NEW ITEMS  -------------------------------------");
        let htmlData = "";
        //if (filterstate == false) {
        //    htmlData = " <li class='active' data-filter='*'>All</li>";
        //    filterstate = true;
        //}
 
     
        for (let i = 0; i < newItems.length; i++) {
            htmlData = htmlData + clsCart.Template1(newItems[i]);
        }

        console.log(htmlData);
        $("#dvPostItems").html(htmlData);


        for (let i = 0; i < newItems.length; i++) {
            $('.set-bg').each(function () {
                var bg = $(this).data('setbg');
                $(this).css('background-image', 'url(' + bg + ')');
                // console.log($(this).data('setbg'));

            });

        }

    }
    ,

    saveInCart: function (itemid) {
        alert("save ");




        var person = new Object();
        person.itemId = itemid;
        for (let i = 0; i < Items.length; i++) {

            if (Items[i].itemId == person.itemId) {
                person.itemName = Items[i].itemName;
                person.Price = Items[i].salesPrice;
                person.imageName = Items[i].imageName;
                break;

            }
            //htmlData = htmlData + clsCart.Template1(data[i]);

            //// ClsItems2.loadImage(data[i]);

        }


        //console.log(person);

        //var person = {
        //    "firstname": "ahmed",
        //    "lastname": "haddad",
        //    "email": "haddad-a@live.fr",
        //    "domainName": "easyappointments-master",
        //    "phoneNumber": "25276164",
        //    "address": "ariana",
        //    "city": "grand tunis",
        //    "zip_code": "4100",
        //};

        var dataJson = JSON.stringify(person);

        $.ajax({
            url: "https://localhost:44354/api/CartApi",

            type: "post",
            contentType: "application/json",
            data: dataJson,
            success: function (result, status, xhr) {
                //alert("done ");

            },
            error: function (xhr, status, error) {
                alert("error");
            }
        });






    },





}

clsCart.saveCart();






