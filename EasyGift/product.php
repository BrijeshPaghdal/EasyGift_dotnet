<?php
require_once 'head.php';
require_once 'header.php';


if (isset($_SESSION['user_id'])) {
  if (isset($_SESSION['city'])) {
    $user_latitude = $_SESSION['latitude'];
    $user_longitude = $_SESSION['longitude'];
  }
}
$prod_id = $_GET['prod_id'];

$tempUrl = $url . 'api/EasyGift/Product/GetProductDetail/' . $prod_id;
$response = fetchRequest($tempUrl);
if ($response['statusCode'] == 200 && count($response['result']) > 0) {
  $row = $response['result'];

  $shop_id = $row['ShopId'];
  $cate_name = $row['CategoryName'];
  $sub_c_name = $row['SubCategoryName'];
  $prod_name = $row['ProductName'];
  $comp_name = $row['CompanyName'];
  $price = $row['Price'];
  $avai_qua = $row['AvailableQuantity'];
  $prod_desc = $row['ProductDiscription'];
  ?>

  <body>
    <div class="page-wrapper">

      <!-- End .header -->

      <main class="main">
        <nav aria-label="breadcrumb" class="breadcrumb-nav border-0 mb-0">
          <div class="container d-flex align-items-center">
            <ol class="breadcrumb">
              <li class="breadcrumb-item"><a href="index.php">Home</a></li>
              <li class="breadcrumb-item active" aria-current="page">
                Products
              </li>
            </ol>
          </div>
          <!-- End .container -->
        </nav>
        <!-- End .breadcrumb-nav -->

        <div class="page-content">
          <div class="container">
            <div class="product-details-top">
              <div class="row">
                <div class="col-md-6">
                  <div class="product-gallery product-gallery-vertical">
                    <div class="row">
                      <figure class="product-main-image">
                        <?php

                        $tempUrl = $url . 'api/EasyGift/Image?filter=ProductId=' . $prod_id;
                        $res = fetchRequest($tempUrl);
                        $image_name = "";
                        if ($res['statusCode'] == 200 && count($res['result']) > 0) {
                          $tmp = 0;
                          foreach ($res['result'] as $row1) {
                            $image_name = $row1['imageName'];
                            break;
                          }
                          echo " <img id='product-zoom' src='./vendor/product-images/{$image_name}' data-zoom-image='./vendor/product-images/{$image_name}' alt='product image'>";
                        }

                        ?>
                        <a href="#" id="btn-product-gallery" class="btn-product-gallery">
                          <i class="icon-arrows"></i>
                        </a>
                      </figure>
                      <!-- End .product-main-image -->

                      <div id="product-zoom-gallery" class="product-image-gallery">

                        <?php
                        $tempUrl = $url . 'api/EasyGift/Image?filter=ProductId=' . $prod_id;
                        $res = fetchRequest($tempUrl);
                        $image_name = "";
                        if ($res['statusCode'] == 200 && count($res['result']) > 0) {
                          $tmp = 0;
                          foreach ($res['result'] as $row1) {
                            if ($tmp == 0) {
                              $image_name = $row1['imageName'];
                              $tmp++;
                              echo "<a class='product-gallery-item active' href='#' data-image='./vendor/product-images/{$image_name}' data-zoom-image='./vendor/product-images/{$image_name}'>
                                            <img src='./vendor/product-images/{$image_name}' alt='product cross' />
                                      </a>";
                            } else {
                              $image_name = $row1['imageName'];
                              $tmp++;
                              echo " <a class='product-gallery-item' href='#' data-image='./vendor/product-images/{$image_name}' data-zoom-image='./vendor/product-images/{$image_name}'>
                                        <img src='./vendor/product-images/{$image_name}' alt='product cross' />
                                      </a>";
                            }
                          }
                        }

                        ?>


                      </div>
                      <!-- End .product-image-gallery -->
                    </div>
                    <!-- End .row -->
                  </div>
                  <!-- End .product-gallery -->
                </div>
                <!-- End .col-md-6 -->

                <div class="col-md-6">
                  <div class="product-details product-details-centered">
                    <h1 class="product-title" id="name<?php echo $prod_id ?>"><?php echo $prod_name ?></h1>
                    <!-- End .product-title -->


                    <div class="ratings-container">
                      <div class="ratings">

                        <?php
                        $tempUrl = $url . 'api/EasyGift/Review/GetProductReviews?ProductId=' . $prod_id;
                        $res2 = fetchRequest($tempUrl);
                        $cnt2 = count($res2['result']);
                        if ($res2['statusCode'] == 200 && count($res2['result']) > 0) {
                          $rating = 0;
                          foreach ($res2['result'] as $row) {
                            $rating += (int) $row['rating'];
                          }
                          $rating = ($rating / ($cnt2 * 5)) * 100;
                          $total_rating = $cnt2;
                        } else {
                          $rating = 0;
                          $total_rating = $cnt2;
                        }
                        ?>

                        <div class="ratings-val" style="width: <?php echo $rating ?>%"></div>

                        <!-- End .ratings-val -->
                      </div>
                      <!-- End .ratings -->

                    </div>
                    <!-- End .rating-container -->

                    <div class="product-price" id="price<?php echo $prod_id ?>"> ₹ <?php echo $price ?></div>
                    <!-- End .product-price -->

                    <div class="product-content">
                      <p>
                        <?php echo $prod_desc ?>
                      </p>
                    </div>
                    <!-- End .product-content -->

                    <div class="product-details-action">
                      <div class="details-action-col">
                        <div class="product-details-quantity">
                          <input type="number" id="qty" class="form-control" value="1" min="1" max=<?php echo $avai_qua ?>
                            step="1" data-decimals="0" required />
                        </div>
                        <!-- End .product-details-quantity -->
                        <?php
                        if ($avai_qua > 0) {
                          ?>
                          <button class="btn btn-block btn-outline-primary-2 btn-product btn-cart"
                            id="<?php echo $prod_id ?>">
                            Add to Cart
                          </button>
                          <?php
                        } else {
                          ?>
                          <label class="btn btn-block btn-outline-primary-2 btn-product" id="<?php echo $prod_id ?>">
                            Out of Stock
                          </label>
                        <?php
                        }
                        ?>
                      </div>
                      <!-- End .details-action-col -->


                    </div>
                    <!-- End .product-details-action -->

                    <div class="product-details-footer">
                      <div class="product-cat">
                        <span>Suggest For : </span>
                        <?php
                        $tempUrl = $url . 'api/EasyGift/ProductSuggestion/GetProductSuggestions/' . $prod_id;
                        $res = fetchRequest($tempUrl);
                        if ($res['statusCode'] == 200 && count($res['result']) > 0) {
                          foreach ($res['result'] as $row) {
                            echo "<a href='#'>{$row['SuggestedFor']}</a> ";
                          }
                        }
                        ?>
                      </div>
                      <!-- End .product-cat -->

                    </div>
                    <!-- End .product-details-footer -->
                  </div>
                  <!-- End .product-details -->

                </div>
                <!-- End .col-md-6 -->
              </div>
              <!-- End .row -->
            </div>
            <!-- End .product-details-top -->

            <div class="product-details-tab">
              <ul class="nav nav-pills justify-content-center" role="tablist">
                <li class="nav-item">
                  <a class="nav-link active" id="product-desc-link" data-toggle="tab" href="#product-desc-tab" role="tab"
                    aria-controls="product-desc-tab" aria-selected="true">Description</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="product-info-link" data-toggle="tab" href="#product-info-tab" role="tab"
                    aria-controls="product-info-tab" aria-selected="false">Shop information</a>
                </li>
                <li class="nav-item">
                  <a class="nav-link" id="product-shipping-link" data-toggle="tab" href="#product-shipping-tab" role="tab"
                    aria-controls="product-shipping-tab" aria-selected="false">Location</a>
                </li>
                <!-- <li class="nav-item">
                  <a class="nav-link" id="product-review-link" data-toggle="tab" href="#product-review-tab" role="tab" aria-controls="product-review-tab" aria-selected="false">Reviews</a>
                </li> -->
              </ul>
              <div class="tab-content">
                <div class="tab-pane fade show active" id="product-desc-tab" role="tabpanel"
                  aria-labelledby="product-desc-link">
                  <div class="product-desc-content">
                    <h3>Product Information</h3>

                    <ul>
                      <li>
                        Category :
                        <?php echo $cate_name ?>
                      </li>
                      <li>
                        Sub Category :
                        <?php echo $sub_c_name ?>
                      </li>
                      <li>
                        Company Name :
                        <?php echo $comp_name ?>
                      </li>
                      <li>
                        Price : ₹
                        <?php echo $price ?>
                      </li>
                    </ul>

                    <p>
                      <?php echo $prod_desc ?>
                    </p>
                  </div>
                  <!-- End .product-desc-content -->
                </div>
                <!-- .End .tab-pane -->
                <?php

                $tempUrl = $url . 'api/EasyGift/Shop/' . $shop_id;
                $res = fetchRequest($tempUrl);
                if ($res['statusCode'] == 200 && count($res['result']) > 0) {
                  $seller_id = $res['result']['sellerId'];
                }
                $tempUrl = $url . 'api/EasyGift/Seller/GetSellerDetails/' . $seller_id;
                $response = fetchRequest($tempUrl);
                if ($response['statusCode'] == 200) {
                  if (count($response['result']) == 0) {
                    header("location:404.php");
                  }
                  if (count($response['result']) > 0) {
                    $row = $response['result'];

                    $seller_name = $row['SellerName'];
                    $seller_l_name = $row['SellerLastName'];
                    $seller_phone_no = $row['SellerPhoneNo'];

                    $shop_name = $row['ShopName'];
                    $latitude = $row['latitude'];
                    $longitude = $row['longitude'];
                    $address = $row['ShopAddress'];
                    $pincode = $row['pincode'];
                    $city_name = $row['CityName'];
                    $state_name = $row['StateName'];
                    ?>
                    <div class="tab-pane fade" id="product-info-tab" role="tabpanel" aria-labelledby="product-info-link">
                      <div class="product-desc-content">
                        <h3>Shop Information</h3>
                        <ul>
                          <li>Shop Name :
                            <?php echo $shop_name ?>
                          </li>
                          <li>Seller Name :
                            <?php echo $seller_name . " " . $seller_l_name ?>
                          </li>
                          <li>Shop Phone No :
                            <?php echo $seller_phone_no ?>
                          </li>
                          <li>Address :
                            <?php echo $address ?>
                          </li>
                          <li>Pincode :
                            <?php echo $pincode ?>
                          </li>
                          <li>City :
                            <?php echo $city_name ?>
                          </li>
                          <li>State :
                            <?php echo $state_name ?>
                          </li>
                        </ul>

                      </div>
                      <!-- End .product-desc-content -->
                    </div>
                    <!-- .End .tab-pane -->
                    <div class="tab-pane fade" id="product-shipping-tab" role="tabpanel"
                      aria-labelledby="product-shipping-link">
                      <div class="product-desc-content">
                        <h3>Location</h3>
                        <input type="hidden" name="latitude" id="latitude" value="<?php echo $latitude ?>">
                        <input type="hidden" name="longitude" id="longitude" value="<?php echo $longitude ?>">
                        <input type="hidden" name="user_latitude" id="user_latitude" value="<?php echo $user_latitude ?>">
                        <input type="hidden" name="user_longitude" id="user_longitude" value="<?php echo $user_longitude ?>">
                        <div class="tab-pane body active" id="map1" style="height:500px">
                        </div>
                      </div>
                      <!-- End .product-desc-content -->
                    </div>
                  <?php
                  }
                }
                ?>
                <!-- .End .tab-pane -->

              </div>
            </div>
            <!-- End .tab-content -->
            <div class="container">
              <div class="reviews">

                <h3>Reviews</h3>
                <?php
                $tempUrl = $url . 'api/EasyGift/Review/GetProductReviews?ProductId=' . $prod_id;
                $response = fetchRequest($tempUrl);
                $temp = 0;
                if ($response['statusCode'] == 200 && count($response['result']) > 0) {

                  if (isset($_SESSION['user_id']))
                    $cust_id = $_SESSION['user_id'];
                  else
                    $cust_id = 0;
                  foreach ($response['result'] as $row) {
                    $c_id = $row['CustomerId'];
                    $cust_name = $row['CustomerName'];
                    $prod_name = $row['ProductName'];
                    $rating = $row['rating'];
                    $review = $row['ReviewDetail'];
                    $review_date = $row['ReviewDate'];
                    $review_date = new DateTime($review_date);
                    $review_date = $review_date->format('d-m-Y');

                    $time = strtotime($review_date);

                    if ($cust_id == $c_id) {
                      $temp = 1;
                    }
                    ?>
                    <div class="review">
                      <div class="row no-gutters">
                        <div class="col-auto">
                          <h4><a href="#">
                              <?php echo $cust_name ?>
                            </a></h4>
                          <div class="ratings-container">
                            <div class="ratings">
                              <div class="ratings-val" style="width: <?php echo $rating * 20 ?>%"></div>
                              <!-- End .ratings-val -->
                            </div>
                            <!-- End .ratings -->
                          </div>
                          <!-- End .rating-container -->
                          <span class="review-date">
                            <?php echo date("d-m-Y", $time) ?>
                          </span>
                        </div>
                        <!-- End .col -->
                        <div class="col">

                          <h4>
                            <?php echo $review ?>
                          </h4>

                          <!-- <div class="review-content">

                                  </div> -->
                          <!-- End .review-content -->


                        </div>
                        <!-- End .col-auto -->
                      </div>
                    <?php }

                  ?>
                  </div>
                  <!-- End .review -->
                  <?php
                } else {
                  echo "<h5> No Review Available </h5>";
                }
                if ($temp ==0 && isset($_SESSION['user_id'])) {
                  $tempUrl = $url . 'api/EasyGift/Order?columns=Id&filter=CustomerId==' . $_SESSION['user_id'] . ' AND ProductId=' . $prod_id;
                  $response = fetchRequest($tempUrl);
                  if ($response['statusCode'] == 200 && count($response['result']) > 0) {
                    foreach ($response['result'] as $row) {
                      $ord_id = $row['Id'];
                    }

                    ?>
                    <!-- End .row -->
                    <div class="mb-3"></div>

                    <form method="post" id="add_review">
                      <div class="row">
                        <h3>Add Review</h3>

                        <div class="ratings-container col-lg-12">
                          <div class="rateyo-readonly-widg"></div>
                          <!-- End .ratings-val -->
                        </div>

                        <div class="col-lg-12">
                          <div class="row">
                            <textarea class="form-control" cols="30" rows="4" placeholder="Keep add reviews" id="review_desc"
                              required></textarea>
                          </div>
                        </div>
                        <input type="hidden" name="ord_id" id="ord_id" value="<?php echo $ord_id ?>">
                        <button type="submit" class="btn btn-outline-primary-2" id="addRating">
                          <span>ADD REVIEW</span>
                        </button>
                      </div>
                      <!-- End .row -->
                  </div>
                  </form>
                  <?php
                  }
                }
                ?>
            </div>
          </div>
          <!-- End .product-details-tab -->

          <h2 class="title text-center mb-4">You May Also Like</h2>
          <!-- End .title text-center -->
          <div class="owl-carousel owl-simple carousel-equal-height carousel-with-shadow" data-toggle="owl"
            data-owl-options='{
                            "nav": false,
                            "dots": true,
                            "margin": 20,
                            "loop": false,
                            "responsive": {
                                "0": {
                                    "items":1
                                },
                                "480": {
                                    "items":2
                                },
                                "768": {
                                    "items":3
                                },
                                "992": {
                                    "items":4
                                },
                                "1200": {
                                    "items":4,
                                    "nav": true,
                                    "dots": false
                                }
                            }
                        }'>

            <?php
            $tempUrl = $url . 'api/EasyGift/Cities/' . $city_name . '/Products?CategoryName=' . $cate_name;
            $response = fetchRequest($tempUrl);
            if ($response['statusCode'] == 200 && count($response['result']) > 0) {
              foreach ($response['result'] as $row) {
                $prod_id = $row['Id'];
                $prod_name = $row['ProductName'];
                $price = $row['Price'];
                ?>
                <div class="product product-7 text-center">
                  <figure class="product-media">


                    <a href="product.php?prod_id=<?php echo $prod_id ?>">
                      <?php
                      $tempUrl = $url . 'api/EasyGift/Image?filter=ProductId=' . $prod_id;
                      $res = fetchRequest($tempUrl);
                      if ($res['statusCode'] == 200 && count($res['result']) > 0) {
                        $image_name = "";
                        foreach ($res['result'] as $row1) {
                          $image_name = $row1['imageName'];
                          echo "<img src='./vendor/product-images/$image_name' alt='Product image' class='product-image'>";
                          break;

                        }
                      }
                      ?>
                    </a>


                    <div class="product-action">
                      <a href="#" id="<?php echo $prod_id ?>" class="btn-product btn-cart"><span>add to cart</span></a>

                    </div>
                    <!-- End .product-action -->
                  </figure>
                  <!-- End .product-media -->

                  <div class="product-body">
                    <div class="product-cat">
                      <a href="#">
                        <?php echo $cate_name ?>
                      </a>
                    </div>
                    <!-- End .product-cat -->
                    <h3 class="product-title">
                      <a href="product.php?prod_id=<?php echo $prod_id ?>"><?php echo $prod_name ?></a>
                    </h3>
                    <!-- End .product-title -->
                    <div class="product-price"> ₹
                      <?php echo $price ?>
                    </div>
                    <!-- End .product-price -->
                    <div class="ratings-container">

                      <div class="ratings">
                        <?php
                        $tempUrl = $url . 'api/EasyGift/Review/GetProductReviews?ProductId=' . $prod_id;
                        $res2 = fetchRequest($tempUrl);
                        $cnt2 = count($res2['result']);
                        if ($res2['statusCode'] == 200 && count($res2['result']) > 0) {
                          $rating = 0;
                          foreach ($res2['result'] as $row) {
                            $rating += (int) $row['rating'];
                          }
                          $rating = ($rating / ($cnt2 * 5)) * 100;
                          $total_rating = $cnt2;
                        } else {
                          $rating = 0;
                          $total_rating = $cnt2;
                        }

                        ?>
                        <div class="ratings-val" style="width: <?php echo $rating ?>%"></div>

                        <?php
                        //   }
                        // } ?>

                        <!-- End .ratings-val -->
                      </div>
                      <!-- End .ratings -->

                    </div>
                    <!-- End .rating-container -->

                  </div>
                  <!-- End .product-body -->
                </div>
                <!-- End .product -->
                <?php
              }
            }
            ?>
          </div>
          <!-- End .owl-carousel -->
        </div>
        <!-- End .container -->
    </div>
    <!-- End .page-content -->
    </main>
    <!-- End .main -->

    <?php require_once 'footer.php'; ?>

    <!-- End .footer -->
    </div>
    <!-- End .page-wrapper -->
    <button id="scroll-top" title="Back to Top">
      <i class="icon-arrow-up"></i>
    </button>

    <!-- Mobile Menu -->
    <?php require_once 'mobile-menu.php'; ?>

    <!-- End .mobile-menu-container -->

    <!-- Sign in / Register Modal -->
    <?php require_once("./signin-modal.php") ?>
    <!-- End .modal -->
  <?php } else {
  header("Location:404.php");
} ?>

  <!-- Plugins JS File -->
  <script src="assets/js/jquery.min.js"></script>
  <script src="assets/js/bootstrap.bundle.min.js"></script>
  <script src="assets/js/jquery.hoverIntent.min.js"></script>
  <script src="assets/js/jquery.waypoints.min.js"></script>
  <script src="assets/js/superfish.min.js"></script>
  <script src="assets/js/owl.carousel.min.js"></script>
  <script src="assets/js/wNumb.js"></script>
  <script src="assets/js/bootstrap-input-spinner.js"></script>
  <script src="assets/js/jquery.elevateZoom.min.js"></script>
  <script src="assets/js/bootstrap-input-spinner.js"></script>
  <script src="assets/js/jquery.magnific-popup.min.js"></script>
  <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAp5TcMKKv687gUTX3s0GJRqtt-KGjgMDo"></script>
  <!-- <script type="text/javascript" src="assets/css/plugins/rateyo/jquery.min.js"></script> -->
  <script type="text/javascript" src="assets/css/plugins/rateyo/jquery.rateyo.js"></script>
  <!-- Main JS File -->
  <script src="assets/js/main.js"></script>
  <!-- Custom Js File -->
  <script src="assets/js/custom.js"></script>
</body>

</html>