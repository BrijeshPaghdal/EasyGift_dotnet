<?php
  $title = "Home";
  include("./header.php");
  $shop_id = $_SESSION['Shop_Id'];
?>
<section class="content">
  <div class="container-fluid">
    <div class="block-header">
      <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
          <ul class="breadcrumb breadcrumb-style">
            <li class="breadcrumb-item">
              <h4 class="page-title"><?php echo $title ?></h4>
            </li>
          </ul>
        </div>
      </div>
    </div>
    <!-- Widgets -->
    <div class="row">
      <div class="col-lg-4 col-sm-4">
        <div class="counter-box text-center white">
          <div class="text font-17 m-b-5">Total Income</div>
          <h3 class="m-b-20">₹
          <?php
          $tempUrl = $url.'api/EasyGift/Shop/GetTotalIncome/'.$shop_id;
          $response = fetchRequest($tempUrl);
          if($response['statusCode'] == 200 && count($response['result'])>0)
          {
            echo $response['result']['Total'];
          }
          ?>
          </h3>
          <div class="icon">
            <div class="chart chart-bar" id="chart-bar">
              <?php
                date_default_timezone_set("Asia/Kolkata");
                $days = 15;
                $tempUrl = $url.'api/EasyGift/Shop/GetTotalIncomeforChart?ShopId='.$shop_id.'&limit='.$days;
                $response = fetchRequest($tempUrl);
                $output = "";

                if($response['statusCode']==200)
                {
                  $output .= getLastDataForChart($response['result'],$days);
                }
                elseif($response['statusCode']==404)
                {
                  echo 0;
                }
                echo $output;
              ?>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-4 col-sm-4">
        <div class="counter-box text-center white">
          <div class="text font-17 m-b-5">Orders Received</div>
          <h3 class="m-b-20">
          <?php
            $tempUrl = $url.'api/EasyGift/shop/GetTotalOrder/'.$shop_id;
            $response = fetchRequest($tempUrl);
            if($response['statusCode']==200)
            {
              if(count($response['result'])>0)
              {
                echo $response['result'][0]['total_order'];
              }
            }
          ?>
          </h3>
          <div class="icon">
            <div class="chart chart-line" id = "chart-line">
              <?php
              	date_default_timezone_set("Asia/Kolkata");
              	$days = 14;
                $tempUrl = $url.'api/EasyGift/Order/GetPastOrder?ShopId='.$shop_id.'&limit='.$days;
                $response = fetchRequest($tempUrl);
                $output = "";
                if($response['statusCode']==200)
                {
                  $output .= getLastDataForChart($response['result'],$days);
                }
                elseif($response['statusCode']==404)
                {
                  echo 0;
                }
                  echo $output;
              ?>

            </div>
          </div>
        </div>
      </div>

      <div class="col-lg-4 col-sm-4">
        <div class="counter-box text-center white">
          <div class="text font-17 m-b-5">Total Sales</div>
          <h3 class="m-b-20">
          <?php
            $tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/GetTotalProductSold/';
            $response = fetchRequest($tempUrl);
            if($response['statusCode']==200)
            {
              if(count($response['result'])>0)
              {
                echo $response['result'][0]['Total'];
              }
            }
          ?>
          </h3>
          <div class="icon">
          <div class = 'chart chart-pie' id="pie-chart">
          <?php
              	date_default_timezone_set("Asia/Kolkata");
              	$days = 7;
                $tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/GetTotalProductSoldforChart?limit='.$days;
                $response = fetchRequest($tempUrl);
                $output = "";
                $i = 1;
                if($response['statusCode']==200)
                {
                  foreach($response['result'] as $row)
                  {
                    $output .= $row['Total'];
                    if ($i >= count($response['result'])) {
                            break;
                          } else {
                            $output .= ",";
                            $i++;
                          }
                  }
                }
                elseif($response['statusCode']==404)
                {
                  echo 0;
                }
                  echo $output;
              ?>
          </div>
          </div>
        </div>
      </div>
    </div>
    <!-- #END# Widgets -->
    <div class="row">
      <div class="col-lg-6 col-sm-6">
          <div class="card">
              <div class="header">
                  <h2>
                      <strong>Past 7 days</strong> Orders</h2>
                      <ul class="header-dropdown m-r--5">
                      <li class="dropdown">
                        <a href="#" onClick="return false;" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                          <i class="material-icons">more_vert</i>
                        </a>
                        <ul class="dropdown-menu pull-right">
                          <li>
                            <a href="order.php">View More</a>
                          </li>
                        </ul>
                      </li>
                    </ul>
              </div>
              <div class="body">
                  <div class="recent-report_chart">
                      <canvas id="line-chart"></canvas>
                  </div>
              </div>
          </div>
      </div>
      <div class="col-lg-6 col-sm-6">
          <div class="card">
              <div class="header">
                  <h2>
                      <strong>Past 7 days</strong> Sold Products</h2>
                      <ul class="header-dropdown m-r--5">
                      <li class="dropdown">
                        <a href="#" onClick="return false;" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                          <i class="material-icons">more_vert</i>
                        </a>
                        <ul class="dropdown-menu pull-right">
                          <li>
                            <a href="order.php">View More</a>
                          </li>
                        </ul>
                      </li>
                    </ul>
              </div>
              <div class="body">
                  <div class="recent-report_chart">
                      <canvas id="pie-chart-big"><?php
                      $days = 7;
                      $tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/GetTotalProductSoldforChart?limit='.$days;
                      $response = fetchRequest($tempUrl);
                      $output = "";
                      $i = 1;
                      if($response['statusCode']==200)
                      {
                        foreach($response['result'] as $row)
                        {
                          $output .= $row['Total'];
                          if ($i >= count($response['result'])) {
                                  break;
                                } else {
                                  $output .= ",";
                                  $i++;
                                }
                        }
                      }
                      elseif($response['statusCode']==404)
                      {
                        echo 0;
                      }
                        echo $output;
                  ?></canvas>
                  </div>
              </div>
          </div>
      </div>
    </div>

    <div class="row clearfix">
      <!-- Browser Usage -->
      <div class="col-xs-12 col-sm-12 col-md-5 col-lg-5">
        <div class="card">
          <div class="header">
            <h2><strong>Recently Added </strong>Products</h2>
            <ul class="header-dropdown m-r--5">
              <li class="dropdown">
                <a href="#" onClick="return false;" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                  <i class="material-icons">more_vert</i>
                </a>
                <ul class="dropdown-menu pull-right">
                  <li>
                    <a href="product-list.php">View More</a>
                  </li>
                </ul>
              </li>
            </ul>
          </div>
          <div class="tableBody">
              <div class="table-responsive" id = 'quick-product-view'>

              </div>
          </div>
        </div>
      </div>
      <!-- #END# Quick View Product -->
      <!-- Quick View Order -->
      <div class="col-xs-12 col-sm-12 col-md-7 col-lg-7">
        <div class="card">
          <div class="header">
            <h2><strong>Recent</strong> Orders</h2>
            <ul class="header-dropdown m-r--5">
              <li class="dropdown">
                <a href="#" onClick="return false;" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                  <i class="material-icons">more_vert</i>
                </a>
                <ul class="dropdown-menu pull-right">
                  <li>
                    <a href="order.php">View More</a>
                  </li>
                </ul>
              </li>
            </ul>
          </div>
          <div class="tableBody">
            <div class="table-responsive" id = 'quick-order-view'>

            </div>
          </div>
        </div>
      </div>
      <!-- #END# Quick View Order -->
    </div>
    <div class="row clearfix">
      <!-- Customer Review -->
      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
        <div class="card">
          <div class="header">
            <h2><strong>Customer</strong> Review</h2>
          </div>
          <div class="body">
            <div class="review-block" id = 'quick-view-review'>

        </div>
      </div>
      <!-- #END# Customer Review -->
    </div>
  </div>
</section>
<!-- Plugins Js -->
<script src="assets/js/app.min.js"></script>
<script src="assets/js/chart.min.js"></script>
<!-- Custom Js -->
<script src="assets/js/admin.js"></script>
<script src="assets/js/pages/index.js"></script>
<script src="assets/js/custom.js" defer></script>
</body>

</html>
