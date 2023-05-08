<?php
require_once("../config.php");
session_start();

$tempUrl = $url.'api/EasyGift/Product/filterProduct';
$data = array();

if(isset($_SESSION['user_id']))
{
  if(isset($_SESSION['city']))
  {
    $data["CityName"] = $_SESSION['city'];
  }
}
if (isset($_POST['minimum_price'])) {
  if ($_POST['minimum_price'] != '') {
    $data["MinPrice"] = (int)$_POST['minimum_price'];
  }
}

if (isset($_POST['maximum_price'])) {
  if ($_POST['maximum_price'] != '') {
    $data["MaxPrice"] = (int)$_POST['maximum_price'];
  }
}

if (isset($_POST['category'])) {
  if ($_POST['category'] != '') {
    $data["CategoryName"] = $_POST['category'];
  }
}

if (isset($_POST['subcategory'])) {
  if ($_POST['subcategory'] != '') {
    $data["SubCategoryName"] = $_POST['subcategory'];
  }
}

if (isset($_POST['cate_id'])) {
  $cate_id = implode(",", $_POST["cate_id"]);
  $data["CategoryIds"] = $cate_id;
}
if (isset($_POST['sugg_id'])) {
  $sugg_id = implode(",", $_POST["sugg_id"]);
  $data["SuggestionIds"] = $sugg_id;
}
if($data==[])
{  $data=json_encode(null);
}
else{
  $data = json_encode($data);
}
$response = addData($tempUrl,$data);

if ($response['statusCode'] == 200 && count($response['result'])>0) {

  $output = '';
  foreach ($response['result'] as $row) {

    $prod_id = $row['Id'];
    $prod_name = $row['ProductName'];
    $price = $row['Price'];
    
    $tempUrl = $url.'api/EasyGift/Image?filter=ProductId='.$prod_id;
    $res = fetchRequest($tempUrl);
    if ($res['statusCode'] == 200 && count($res['result'])>0) {
      $image_name = "";
      foreach ($res['result'] as $row1) {
        $image_name = $row1['imageName'];
        break;
      }
    }

    $output .= '<div class="col-6 col-md-4 col-lg-4 col-xl-3">
                    <div class="product text-center">
                    <figure class="product-media">

              <a href="product.php?prod_id=' . $prod_id . '">
                <img src="./vendor/product-images/' . $image_name . '" alt="Product image" class="product-image" />
              </a>';

    $output .= '<div class="product-action">';
    if($row['AvailableQuantity'] > 0)
    {
      $output .= '<a href="" id=' . $prod_id . ' class="btn-product btn-cart"><span>add to cart</span></a>';
    }
    else
    {
      $output .= '<span class="btn-product btn-cart">sold out</span>';
    }
    $output .= '</div>
              <!-- End .product-action -->
            </figure>
            <!-- End .product-media -->

            <div class="product-body">
              <!-- End .product-cat -->
              <h3 class="product-title">
                <a href="product.php?prod_id=' . $prod_id . '">' . $prod_name . '</a>
              </h3>
              <!-- End .product-title -->
              <div class="product-price">₹' . $price . '</div>
              <!-- End .product-price -->';
    $tempUrl = $url.'api/EasyGift/Review/GetProductReviews?ProductId='.$prod_id;
    $res2 = fetchRequest($tempUrl);
    $cnt2= count($res2['result']);
    if ($res2['statusCode']==200 && count($res2['result']) > 0) {
    $rating = 0;
    foreach ($res2['result'] as $row) {
    $rating += (int)$row['rating'];
    }
    $rating = ($rating / ($cnt2 * 5)) * 100;
    $total_rating = $cnt2;
    } else {
    $rating = 0;
    $total_rating = $cnt2;
    } 

    $output .= '  <div class="ratings-container">
                <div class="ratings">
                  <div class="ratings-val" style="width:' . $rating . '%"></div>
                  <!-- End .ratings-val -->
                </div>
                <!-- End .ratings -->
                </div>
                <div class="ratings-container">
                <span class="ratings-text">( ' . $total_rating . ' Reviews )</span>
              </div>
              <!-- End .rating-container -->
            </div>';
    $output .= '<!-- End .product-body -->
                          </div>
                          <!-- End .product -->
                        </div>';
  }
  echo $output;
} else {
  echo "No Product Available";
}
