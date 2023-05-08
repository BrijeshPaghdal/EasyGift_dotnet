<?php
require_once 'head.php';
// require_once('./config.php');

  session_write_close();
?>

<body>
  <div class="page-wrapper">
    <?php require_once 'header.php'; ?>
    <!-- End .header -->

    <main class="main">
      <div class="intro-slider-container">
        <div class="owl-carousel owl-simple owl-light owl-nav-inside" data-toggle="owl" data-owl-options='{"nav": false}'>
          <div class="intro-slide" style="
                background-image: url(assets/images/demos/demo-7/slider/slide-2.jpg);
              ">
          </div>
          <!-- End .intro-slide -->

          <div class="intro-slide" style="
                background-image: url(assets/images/demos/demo-7/slider/slide-4.jpg);
              ">

            <!-- End .container intro-content -->
          </div>
          <!-- End .intro-slide -->

          <div class="intro-slide" style="
                background-image: url(assets/images/demos/demo-7/slider/slide-3.jpg);
              ">

            <!-- End .container intro-content -->
          </div>
          <!-- End .intro-slide -->

          <div class="intro-slide" style="
                background-image: url(assets/images/demos/demo-7/slider/slide-5.jpg);
              ">
          </div>
          <!-- End .intro-slide -->
          <div class="intro-slide" style="
                background-image: url(assets/images/demos/demo-7/slider/slide-6.jpg);
              ">
          </div>
          <!-- End .intro-slide -->
          <div class="intro-slide" style="
                background-image: url(assets/images/demos/demo-7/slider/slide-8.jpg);
              ">

          </div>
          <!-- End .intro-slide -->
        </div>
        <!-- End .owl-carousel owl-simple -->

        <span class="slider-loader text-white"></span><!-- End .slider-loader -->
      </div>
      <!-- End .intro-slider-container -->
      <div class="page-content">

        <div class="mb-5"></div>

        <div class="bg-light-2 pt-6 pb-6 featured">
          <div class="container-fluid">
            <div class="heading heading-center mb-3">
              <h2 class="title">Most Relevent Product </h2><!-- End .title -->
            </div><!-- End .heading -->

            <div class="tab-content tab-content-carousel">
              <div class="tab-pane p-0 fade show active" id="featured-women-tab" role="tabpanel" aria-labelledby="featured-women-link">
                <div class="owl-carousel owl-simple carousel-equal-height carousel-with-shadow" data-toggle="owl" data-owl-options='{
                                  "nav": false,
                                  "dots": true,
                                  "margin": 20,
                                  "loop": false,
                                  "responsive": {
                                      "0": {
                                          "items":2
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
                                          "items":5,
                                          "nav": true
                                      }
                                  }
                              }'>

                  <?php
                  function getHomePageProduct($url){
                    if(isset($_SESSION['user_id']))
                    {
                      if(isset($_SESSION['city']))
                      {
                        return $url . 'api/EasyGift/Cities/'.$_SESSION['city'].'/Products';
                      }
                      else
                      {
                        return $url . 'api/EasyGift/Cities/null/Products';
                      }
                    }
                    else
                    {
                      return $url . 'api/EasyGift/Cities/null/Products';
                    }
                  }
                  $tempUrl = getHomePageProduct($url);
                  $response = fetchRequest($tempUrl);
                  if ($response['statusCode'] == 200 && count($response['result'])>0) {
                    $output = '';
                    foreach ($response['result'] as $row) {

                      $prod_id = $row['Id'];
                      $prod_name = $row['ProductName'];
                      $price = $row['Price'];
                  ?>
                      <div class="product product-7 text-center">
                        <figure class="product-media">
                          <a href="product.php?prod_id=<?php echo $prod_id ?>">
                            <?php

                            $tempUrl = $url.'api/EasyGift/Image?filter=ProductId='.$prod_id;
                            $res = fetchRequest($tempUrl);
                            if ($res['statusCode'] == 200 && count($res['result'])>0) {
                              $tmp = 0;
                              $image_name = "";
                              foreach ($res['result'] as $row1) {
                                $image_name = $row1['imageName'];
                                if ($tmp == 0) {
                                  $tmp++;
                                  echo "<img src='./vendor/product-images/$image_name' alt='Product image' class='product-image'>";
                                } else if ($tmp == 1) {
                                  $tmp++;
                                  echo "<img src='./vendor/product-images/$image_name' alt='Product image' class='product-image-hover'>";
                                }
                              }
                            }
                            ?>
                          </a>
                          <input type="hidden" id="qty" class="form-control" value="1" min="1" max="10" step="1" data-decimals="0" required />
                          <div class="product-action-vertical">

                            <a href="product.php?prod_id=<?php echo $prod_id ?>" class=" btn-product-icon btn-quickview" title="Quick view"><span>Quick view</span></a>
                          </div><!-- End .product-action-vertical -->

                          <div class="product-action">
                            <a id="<?php echo $prod_id ?>" class="btn-product btn-cart"><span>add to cart</span></a>
                          </div><!-- End .product-action -->
                        </figure><!-- End .product-media -->

                        <div class="product-body">
                          <h3 class="product-title"><a href="product.php?prod_id=<?php echo $prod_id ?>"><?php echo $prod_name ?></a></h3><!-- End .product-title -->
                          <div class="product-price">
                            ₹ <?php echo $price ?>
                          </div><!-- End .product-price -->
                          <?php
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
                          ?>
                          <div class="ratings-container">
                            <div class="ratings">
                              <div class="ratings-val" style="width: <?php echo $rating ?>%;"></div><!-- End .ratings-val -->
                            </div><!-- End .ratings -->
                            <span class="ratings-text">( <?php echo $total_rating ?> Reviews)</span>

                          </div><!-- End .rating-container -->
                        </div><!-- End .product-body -->
                      </div><!-- End .product -->
                  <?php }
                  }
                  ?>
                </div><!-- End .owl-carousel -->
              </div><!-- .End .tab-pane -->
            </div><!-- End .tab-content -->
          </div><!-- End .container-fluid -->
        </div><!-- End .bg-light-2 pt-4 pb-4 -->

        <div class="mb-6"></div><!-- End .mb-6 -->


        <div class="container">

          <hr class="mb-4">

          <h2 class="title text-center mb-3">Shop By Best Categories</h2><!-- End .title mb-2 -->

          <div class="owl-carousel owl-simple" data-toggle="owl" data-owl-options='{
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
            $tempUrl = $url . 'api/EasyGift/Category?filter=Id<>0';
            $response = fetchRequest($tempUrl);
            if ($response['statusCode'] == 200 && count($response['result'])>0) {
              foreach ($response['result'] as $row) {
                $image_name = $row['categoryImageName'];
                $cate_name = $row['categoryName'];
            ?>

                <div class="banner banner-cat">
                  <a href="filter-product.php?category=<?php echo $cate_name ?>">
                    <img src="admin/category-image/<?php echo $image_name ?>" alt="Banner">
                  </a>

                  <div class="banner-content banner-content-static text-center">
                    <h3 class="banner-title"><?php echo $cate_name ?></h3><!-- End .banner-title -->

                  </div><!-- End .banner-content -->
                </div><!-- End .banner -->

            <?php }
            } ?>

          </div><!-- End .banners-carousel owl-carousel owl-simple -->

          <hr class="mb-4">
        </div><!-- End .container -->

      </div><!-- End .page-content -->


      <div class="icon-boxes-container bg-transparent">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-lg-8 col-12 icon-boxes">
                <div class=" col-sm-6 col-lg-4">
              <div class="icon-box icon-box-side">
                <span class="icon-box-icon">
                  <i class="icon-gift"></i>
                </span>

                <div class="icon-box-content">
                  <h3 class="icon-box-title">Easy Received</h3>
                  <!-- End .icon-box-title -->
                  <p>Easy received product near by shop</p>
                </div>
                <!-- End .icon-box-content -->
              </div>
              <!-- End .icon-box -->
            </div>
            <!-- End .col-sm-6 col-lg-4 -->

            <div class="col-sm-6 col-lg-4">
              <div class="icon-box icon-box-side">
                <span class="icon-box-icon">
                  <i class="icon-rotate-left"></i>
                </span>

                <div class="icon-box-content">
                  <h3 class="icon-box-title">Verious gifts</h3>
                  <!-- End .icon-box-title -->
                  <p>Various categories vice gifts available</p>
                </div>
                <!-- End .icon-box-content -->
              </div>
              <!-- End .icon-box -->
            </div>
            <!-- End .col-sm-6 col-lg-4 -->

            <div class="col-sm-6 col-lg-4">
              <div class="icon-box icon-box-side">
                <span class="icon-box-icon">
                  <i class="icon-map-marker"></i>
                </span>

                <div class="icon-box-content">
                  <h3 class="icon-box-title">Near By</h3>
                  <!-- End .icon-box-title -->
                  <p>Near by shop gift view</p>
                </div>
                <!-- End .icon-box-content -->
              </div>
              <!-- End .icon-box -->
            </div>
            <!-- End .col-sm-6 col-lg-4 -->
          </div>
        </div>
        <!-- End .row -->
      </div>
      <!-- End .container -->
  </div>
  <!-- End .icon-boxes-container -->


  <!-- End .bg-light-2 pt-4 pb-4 -->

  <div class="mb-6"></div>
  <!-- End .mb-6 -->
  <div class="bg-light-2 pt-6 pb-6 featured">
    <div class="container-fluid">
      <div class="heading heading-center mb-3">
        <h2 class="title"> Cakes </h2><!-- End .title -->
      </div><!-- End .heading -->

      <div class="tab-content tab-content-carousel">
        <div class="tab-pane p-0 fade show active" id="featured-women-tab" role="tabpanel" aria-labelledby="featured-women-link">
          <div class="owl-carousel owl-simple carousel-equal-height carousel-with-shadow" data-toggle="owl" data-owl-options='{
                                  "nav": false,
                                  "dots": true,
                                  "margin": 20,
                                  "loop": false,
                                  "responsive": {
                                      "0": {
                                          "items":2
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
                                          "items":5,
                                          "nav": true
                                      }
                                  }
                              }'>

            <?php
                  
            $tempUrl = getHomePageProduct($url);
            $tempUrl .= '?CategoryName=Cake';
            $response = fetchRequest($tempUrl);
            if ($response['statusCode'] == 200 && count($response['result'])>0) {
              $output = '';
              foreach ($response['result'] as $row) {

                $prod_id = $row['Id'];
                $prod_name = $row['ProductName'];
                $price = $row['Price'];
            ?>
                <div class="product product-7 text-center">
                  <figure class="product-media">
                    <a href="product.php?prod_id=<?php echo $prod_id ?>">
                      <?php
                       $tempUrl = $url.'api/EasyGift/Image?filter=ProductId='.$prod_id;
                       $res = fetchRequest($tempUrl);
                       if ($res['statusCode'] == 200 && count($res['result'])>0) {
                         $tmp = 0;
                         $image_name = "";
                         foreach ($res['result'] as $row1) {
                           $image_name = $row1['imageName'];
                           if ($tmp == 0) {
                             $tmp++;
                             echo "<img src='./vendor/product-images/$image_name' alt='Product image' class='product-image'>";
                           } else if ($tmp == 1) {
                             $tmp++;
                             echo "<img src='./vendor/product-images/$image_name' alt='Product image' class='product-image-hover'>";
                           }
                         }
                       }
                      ?>
                    </a>

                    <div class="product-action-vertical">

                      <a href="product.php?prod_id=<?php echo $prod_id ?>" class=" btn-product-icon btn-quickview" title="Quick view"><span>Quick view</span></a>
                    </div><!-- End .product-action-vertical -->

                    <div class="product-action">
                      <a id="<?php echo $prod_id ?>" class="btn-product btn-cart"><span>add to cart</span></a>
                    </div><!-- End .product-action -->
                  </figure><!-- End .product-media -->

                  <div class="product-body">
                    <h3 class="product-title"><a href="product.php?prod_id=<?php echo $prod_id ?>"><?php echo $prod_name ?></a></h3><!-- End .product-title -->
                    <div class="product-price">
                      ₹ <?php echo $price ?>
                    </div><!-- End .product-price -->
                    <?php
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
                   }  ?>
                    <div class="ratings-container">
                      <div class="ratings">
                        <div class="ratings-val" style="width: <?php echo $rating ?>%;"></div><!-- End .ratings-val -->
                      </div><!-- End .ratings -->
                      <span class="ratings-text">( <?php echo $total_rating ?> Reviews)</span>

                    </div><!-- End .rating-container -->
                  </div><!-- End .product-body -->
                </div><!-- End .product -->
            <?php }
            }
            ?>
          </div><!-- End .owl-carousel -->
        </div><!-- .End .tab-pane -->
        <div class="tab-pane p-0 fade" id="featured-men-tab" role="tabpanel" aria-labelledby="featured-men-link">
          <div class="owl-carousel owl-simple carousel-equal-height carousel-with-shadow" data-toggle="owl" data-owl-options="{
                                  "nav": false,
                                  "dots": true,
                                  "margin": 20,
                                  "loop": false,
                                  "responsive": {
                                      "0": {
                                          "items":2
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
                                          "items":5,
                                          "nav": true
                                      }
                                  }
                              }">

            <?php
            $tempUrl= getHomePageProduct($url);
            $response = fetchRequest($tempUrl);
            if ($response['statusCode'] == 200 && count($response['result'])>0) {
              $output = '';
              foreach ($response['result'] as $row) {

                $prod_id = $row['Id'];
                $prod_name = $row['ProductName'];
                $price = $row['Price'];
            ?>
                <div class="product product-7 text-center">
                  <figure class="product-media">
                    <a href="product.php?prod_id=<?php echo $prod_id ?>">
                      <?php
                        $tempUrl = $url.'api/EasyGift/Image?filter=ProductId='.$prod_id;
                        $res = fetchRequest($tempUrl);
                        if ($res['statusCode'] == 200 && count($res['result'])>0) {
                          $tmp = 0;
                          $image_name = "";
                          foreach ($res['result'] as $row1) {
                            $image_name = $row1['imageName'];
                            if ($tmp == 0) {
                              $tmp++;
                              echo "<img src='./vendor/product-images/$image_name' alt='Product image' class='product-image'>";
                            } else if ($tmp == 1) {
                              $tmp++;
                              echo "<img src='./vendor/product-images/$image_name' alt='Product image' class='product-image-hover'>";
                            }
                          }
                        }
                      ?>
                    </a>

                    <div class="product-action-vertical">

                      <a href="product.php?prod_id=<?php echo $prod_id ?>" class=" btn-product-icon btn-quickview" title="Quick view"><span>Quick view</span></a>
                    </div><!-- End .product-action-vertical -->

                    <div class="product-action">
                      <a id="<?php echo $prod_id ?>" class="btn-product btn-cart"><span>add to cart</span></a>
                    </div><!-- End .product-action -->
                  </figure><!-- End .product-media -->

                  <div class="product-body">
                    <h3 class="product-title"><a href="product.php?prod_id=<?php echo $prod_id ?>"><?php echo $prod_name ?></a></h3><!-- End .product-title -->
                    <div class="product-price">`
                      ₹ <?php echo $price ?>
                    </div><!-- End .product-price -->
                    <?php
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
                    }  ?>
                    <div class="ratings-container">
                      <div class="ratings">
                        <div class="ratings-val" style="width: <?php echo $rating ?>%;"></div><!-- End .ratings-val -->
                      </div><!-- End .ratings -->
                      <span class="ratings-text">( <?php echo $total_rating ?> Reviews)</span>

                    </div><!-- End .rating-container -->
                  </div><!-- End .product-body -->
                </div><!-- End .product -->
            <?php }
            }
            ?>
          </div><!-- End .owl-carousel -->
        </div><!-- .End .tab-pane -->
      </div><!-- End .tab-content -->
    </div><!-- End .container-fluid -->
  </div><!-- End .bg-light-2 pt-4 pb-4 -->

<!-- End .mb-6 -->

  <div class="bg-light-2 pt-6 pb-6 featured">
    <div class="container-fluid">
      <div class="heading heading-center mb-3">
        <h2 class="title">Flowers </h2><!-- End .title -->
      </div><!-- End .heading -->

      <div class="tab-content tab-content-carousel">
        <div class="tab-pane p-0 fade show active" id="featured-women-tab" role="tabpanel" aria-labelledby="featured-women-link">
          <div class="owl-carousel owl-simple carousel-equal-height carousel-with-shadow" data-toggle="owl" data-owl-options='{
                                  "nav": false,
                                  "dots": true,
                                  "margin": 20,
                                  "loop": false,
                                  "responsive": {
                                      "0": {
                                          "items":2
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
                                          "items":5,
                                          "nav": true
                                      }
                                  }
                              }'>

            <?php
           $tempUrl = $url . 'api/EasyGift/Cities/null/Products?CategoryName=Flowers';
                  
           if(isset($_SESSION['user_id']))
           {
             if(isset($_SESSION['city']))
             {
               $tempUrl = $url . 'api/EasyGift/Cities/'.$_SESSION['city'].'/Products?CategoryName=Flowers';
             }
             else
             {
               $tempUrl = $url . 'api/EasyGift/Cities/null/Products?CategoryName=Flowers';
             }
           }
           else
           {
             $tempUrl = $url . 'api/EasyGift/Cities/null/Products?CategoryName=Flowers';
           }
           $response = fetchRequest($tempUrl);
           if ($response['statusCode'] == 200 && count($response['result'])>0) {
             $output = '';
             foreach ($response['result'] as $row) {

               $prod_id = $row['Id'];
               $prod_name = $row['ProductName'];
               $price = $row['Price'];
            ?>
                <div class="product product-7 text-center">
                  <figure class="product-media">
                    <a href="product.php?prod_id=<?php echo $prod_id ?>">
                      <?php
                        $tempUrl = $url.'api/EasyGift/Image?filter=ProductId='.$prod_id;
                        $res = fetchRequest($tempUrl);
                        if ($res['statusCode'] == 200 && count($res['result'])>0) {
                          $tmp = 0;
                          $image_name = "";
                          foreach ($res['result'] as $row1) {
                            $image_name = $row1['imageName'];
                            if ($tmp == 0) {
                              $tmp++;
                              echo "<img src='./vendor/product-images/$image_name' alt='Product image' class='product-image'>";
                            } else if ($tmp == 1) {
                              $tmp++;
                              echo "<img src='./vendor/product-images/$image_name' alt='Product image' class='product-image-hover'>";
                            }
                          }
                        }
                      ?>
                    </a>

                    <div class="product-action-vertical">

                      <a href="product.php?prod_id=<?php echo $prod_id ?>" class=" btn-product-icon btn-quickview" title="Quick view"><span>Quick view</span></a>
                    </div><!-- End .product-action-vertical -->

                    <div class="product-action">
                      <a id="<?php echo $prod_id ?>" class="btn-product btn-cart"><span>add to cart</span></a>
                    </div><!-- End .product-action -->
                  </figure><!-- End .product-media -->

                  <div class="product-body">
                    <h3 class="product-title"><a href="product.php?prod_id=<?php echo $prod_id ?>"><?php echo $prod_name ?></a></h3><!-- End .product-title -->
                    <div class="product-price">
                      ₹ <?php echo $price ?>
                    </div><!-- End .product-price -->
                    <?php
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
                   }  ?>
                    <div class="ratings-container">
                      <div class="ratings">
                        <div class="ratings-val" style="width: <?php echo $rating ?>%;"></div><!-- End .ratings-val -->
                      </div><!-- End .ratings -->
                      <span class="ratings-text">( <?php echo $total_rating ?> Reviews)</span>

                    </div><!-- End .rating-container -->
                  </div><!-- End .product-body -->
                </div><!-- End .product -->
            <?php }
            }
            ?>
          </div><!-- End .owl-carousel -->
        </div><!-- .End .tab-pane -->
        <div class="tab-pane p-0 fade" id="featured-men-tab" role="tabpanel" aria-labelledby="featured-men-link">
          <div class="owl-carousel owl-simple carousel-equal-height carousel-with-shadow" data-toggle="owl" data-owl-options='{
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
                                      "items":5,
                                      "nav": true
                                  }
                              }
                          }'>
            <div class="product product-7 text-center">
              <figure class="product-media">
                <a href="product.php?prod_id=<?php echo $prod_id ?>">
                  <img src="assets/images/demos/demo-7/products/product-2-1.jpg" alt="Product image" class="product-image">
                  <img src="assets/images/demos/demo-7/products/product-2-2.jpg" alt="Product image" class="product-image-hover">
                </a>

                <div class="product-action-vertical">
                  <a href="#" class="btn-product-icon btn-wishlist btn-expandable"><span>add to wishlist</span></a>
                  <a href="popup/quickView.html" class="btn-product-icon btn-quickview" title="Quick view"><span>Quick view</span></a>
                </div><!-- End .product-action-vertical -->

                <div class="product-action">
                  <a href="#" class="btn-product btn-cart"><span>add to cart</span></a>
                </div><!-- End .product-action -->
              </figure><!-- End .product-media -->

              <div class="product-body">
                <h3 class="product-title"><a href="product.php?prod_id=<?php echo $prod_id ?>">Biker jacket</a></h3><!-- End .product-title -->
                <div class="product-price">
                  $120.99
                </div><!-- End .product-price -->
                <div class="ratings-container">
                  <div class="ratings">
                    <div class="ratings-val" style="width: 20%;"></div><!-- End .ratings-val -->
                  </div><!-- End .ratings -->
                  <span class="ratings-text">( 2 Reviews )</span>
                </div><!-- End .rating-container -->

                <div class="product-nav product-nav-dots">
                  <a href="#" class="active" style="background: #d79442;"><span class="sr-only">Color name</span></a>
                  <a href="#" style="background: #cc3333;"><span class="sr-only">Color name</span></a>
                </div><!-- End .product-nav -->
              </div><!-- End .product-body -->
            </div><!-- End .product -->
            <div class="product product-7 text-center">
              <figure class="product-media">
                <a href="product.php?prod_id=<?php echo $prod_id ?>">
                  <img src="assets/images/demos/demo-7/products/product-3-1.jpg" alt="Product image" class="product-image">
                  <img src="assets/images/demos/demo-7/products/product-3-2.jpg" alt="Product image" class="product-image-hover">
                </a>

                <div class="product-action-vertical">
                  <a href="#" class="btn-product-icon btn-wishlist btn-expandable"><span>add to wishlist</span></a>
                  <a href="popup/quickView.html" class="btn-product-icon btn-quickview" title="Quick view"><span>Quick view</span></a>
                </div><!-- End .product-action-vertical -->

                <div class="product-action">
                  <a href="#" class="btn-product btn-cart"><span>add to cart</span></a>
                </div><!-- End .product-action -->
              </figure><!-- End .product-media -->

              <div class="product-body">
                <h3 class="product-title"><a href="product.php?prod_id=<?php echo $prod_id ?>">Sandals</a></h3><!-- End .product-title -->
                <div class="product-price">
                  $70.00
                </div><!-- End .product-price -->
                <div class="ratings-container">
                  <div class="ratings">
                    <div class="ratings-val" style="width: 60%;"></div><!-- End .ratings-val -->
                  </div><!-- End .ratings -->
                  <span class="ratings-text">( 4 Reviews )</span>
                </div><!-- End .rating-container -->
              </div><!-- End .product-body -->
            </div><!-- End .product -->
            <div class="product product-7 text-center">
              <figure class="product-media">
                <span class="product-label label-circle label-sale">Sale</span>
                <a href="product.php?prod_id=<?php echo $prod_id ?>">
                  <img src="assets/images/demos/demo-7/products/product-4-1.jpg" alt="Product image" class="product-image">
                  <img src="assets/images/demos/demo-7/products/product-4-2.jpg" alt="Product image" class="product-image-hover">
                </a>

                <div class="product-action-vertical">
                  <a href="#" class="btn-product-icon btn-wishlist btn-expandable"><span>add to wishlist</span></a>
                  <a href="popup/quickView.html" class="btn-product-icon btn-quickview" title="Quick view"><span>Quick view</span></a>
                </div><!-- End .product-action-vertical -->

                <div class="product-action">
                  <a href="#" class="btn-product btn-cart"><span>add to cart</span></a>
                </div><!-- End .product-action -->

                <div class="deal-countdown offer-countdown" data-until="+11d"></div><!-- End .deal-countdown -->
              </figure><!-- End .product-media -->

              <div class="product-body">
                <h3 class="product-title"><a href="product.php?prod_id=<?php echo $prod_id ?>">Super Skinny High Jeggings</a></h3><!-- End .product-title -->
                <div class="product-price">
                  <span class="new-price">$190.00</span>
                  <span class="old-price">$310.00</span>
                </div><!-- End .product-price -->
                <div class="ratings-container">
                  <div class="ratings">
                    <div class="ratings-val" style="width: 40%;"></div><!-- End .ratings-val -->
                  </div><!-- End .ratings -->
                  <span class="ratings-text">( 4 Reviews )</span>
                </div><!-- End .rating-container -->
              </div><!-- End .product-body -->
            </div><!-- End .product -->
          </div><!-- End .owl-carousel -->
        </div><!-- .End .tab-pane -->
      </div><!-- End .tab-content -->
    </div><!-- End .container-fluid -->
  </div><!-- End .bg-light-2 pt-4 pb-4 -->

  <div class="mb-6"></div><!-- End .mb-6 -->



  <div class="brands-border owl-carousel owl-simple" data-toggle="owl" data-owl-options='{
                    "nav": false,
                    "dots": false,
                    "margin": 0,
                    "loop": false,
                    "responsive": {
                        "0": {
                            "items":2
                        },
                        "420": {
                            "items":3
                        },
                        "600": {
                            "items":4
                        },
                        "900": {
                            "items":5
                        },
                        "1024": {
                            "items":6
                        },
                        "1360": {
                            "items":7
                        }
                    }
                }'>
    <a href="#" class="brand">
      <img src="assets/images/brands/1.png" alt="Brand Name" />
    </a>

    <a href="#" class="brand">
      <img src="assets/images/brands/2.png" alt="Brand Name" />
    </a>

    <a href="#" class="brand">
      <img src="assets/images/brands/3.png" alt="Brand Name" />
    </a>

    <a href="#" class="brand">
      <img src="assets/images/brands/4.png" alt="Brand Name" />
    </a>

    <a href="#" class="brand">
      <img src="assets/images/brands/5.png" alt="Brand Name" />
    </a>

    <a href="#" class="brand">
      <img src="assets/images/brands/6.png" alt="Brand Name" />
    </a>

    <a href="#" class="brand">
      <img src="assets/images/brands/7.png" alt="Brand Name" />
    </a>
  </div>
  <!-- End .owl-carousel -->
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

  <!-- Sign in / Register Modal -->
  <?php require_once("./signin-modal.php") ?>

  <div class="container newsletter-popup-container mfp-hide" id="newsletter-popup-form">
    <div class="row justify-content-center">
      <div class="col-10">
        <div class="row no-gutters bg-white newsletter-popup-content">
          <div class="col-xl-3-5col col-lg-7 banner-content-wrap">
            <div class="banner-content text-center">
              <img src="assets/images/popup/newsletter/logo.png" class="logo" alt="logo" width="60" height="15" />
              <h2 class="banner-title">
                get <span>25<light>%</light></span> off
              </h2>
              <p>
                Subscribe to the EasyGift eCommerce newsletter to receive timely
                updates from your favorite products.
              </p>
              <form action="#">
                <div class="input-group input-group-round">
                  <input type="email" class="form-control form-control-white" placeholder="Your Email Address" aria-label="Email Adress" required />
                  <div class="input-group-append">
                    <button class="btn" type="submit"><span>go</span></button>
                  </div>
                  <!-- .End .input-group-append -->
                </div>
                <!-- .End .input-group -->
              </form>
              <div class="custom-control custom-checkbox">
                <input type="checkbox" class="custom-control-input" id="register-policy-2" required />
                <label class="custom-control-label" for="register-policy-2">Do not show this popup again</label>
              </div>
              <!-- End .custom-checkbox -->
            </div>
          </div>
          <div class="col-xl-2-5col col-lg-5">
            <img src="assets/images/popup/newsletter/img-1.jpg" class="newsletter-img" alt="newsletter" />
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- Plugins JS File -->
  <script src="assets/js/jquery.min.js"></script>
  <script src="assets/js/bootstrap.bundle.min.js"></script>
  <script src="assets/js/jquery.hoverIntent.min.js"></script>
  <script src="assets/js/jquery.waypoints.min.js"></script>
  <script src="assets/js/superfish.min.js"></script>
  <script src="assets/js/bootstrap-input-spinner.js"></script>
  <script src="assets/js/owl.carousel.min.js"></script>
  <script src="assets/js/jquery.plugin.min.js"></script>
  <script src="assets/js/jquery.countdown.min.js"></script>
  <!-- Main JS File -->
  <script src="assets/js/main.js"></script>
  <script src="assets/js/demos/demo-7.js"></script>
  <!-- Custom Js File -->
  <script src="assets/js/custom.js" defer></script>
</body>



</html>
