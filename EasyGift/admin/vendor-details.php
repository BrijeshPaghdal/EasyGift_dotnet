<?php
ob_start(); 
$title = "Vendor Details";
require_once('./header.php');
require_once('../config.php');
$cnt=1;
$seller_id = (int)$_GET['vendor_id'];
$GetSellerDetailsUrl = $url.'api/EasyGift/Seller/GetSellerDetails/'.$seller_id;
$response = fetchRequest($GetSellerDetailsUrl);
  if($response['statusCode']==200)
  {
    if(count($response['result'])==0)
    {
        header("location:404.php");
    }
    if(count($response['result'])>0)
    {
            $seller_name = $response['result']['SellerName'];
            $seller_l_name = $response['result']['SellerLastName'];
            $seller_phone_no = $response['result']['SellerPhoneNo'];
            $seller_pancard_no = $response['result']['SellerPancardNo'];
            $seller_image = $response['result']['SellerImage'];
        
            $email_id = $response['result']['EmailId'];
            $shop_id = $response['result']['ShopId'];
            $shop_name = $response['result']['ShopName'];
            $gst_no = $response['result']['GSTNo'];
            $latitude = $response['result']['latitude'];
            $longitude = $response['result']['longitude'];
        
            $address = $response['result']['ShopAddress'];
            $pincode = $response['result']['pincode'];
        
            $city_name = $response['result']['CityName'];
        
            $state_name = $response['result']['StateName'];
        
            $country_name = $response['result']['CountryName'];
            $phone_code = $response['result']['PhoneCode'];
        
            $b_acc_no = $response['result']['BankAccountNo'];
        
            $bank_id = $response['result']['BankId'];
            $bank_name = $response['result']['BankName'];
            $bank_ifsc = $response['result']['BankIFSC'];
            $bank_branch = $response['result']['BankBranch'];
            $bank_address = $response['result']['BankAddress'];
            $bank_district = $response['result']['BankDistrict'];
            $bank_city = $response['result']['BankCity'];
            $bank_country = $response['result']['BankCountry'];
            $bank_state = $response['result']['BankState'];
      
        ?>
         <section class="content">
        <div class="container-fluid">
            <div class="block-header">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <ul class="breadcrumb breadcrumb-style ">
                            <li class="breadcrumb-item">
                                <h4 class="page-title"><?php echo $title ?></h4>
                            </li>
                            <li class="breadcrumb-item bcrumb-1">
                                <a href="./index.php">
                                    <i class="fas fa-home"></i>Home</a>
                            </li>
                            <li class="breadcrumb-item active"><?php echo $title ?></li>
                        </ul>
                    </div>
                </div>
            </div>
            <!-- Your content goes here  -->
            <div class="row clearfix">
                <div class="col-lg-5 col-md-12">
                    <div class="card">
                        <div class="m-b-20">
                            <div class="contact-grid">
                                <div class="profile-header bg-dark">
                                    <div class="user-name"><?php
                                                            if ($shop_name)
                                                                echo $shop_name;
                                                            else
                                                                echo "-"; ?></div>
                                    <div class="name-center"><?php echo $seller_name . " " . $seller_l_name ?></div>
                                </div>
                                <img src="../vendor/seller-images/<?php echo $seller_image ?>" class="user-img" alt="">
                                <p style="padding:10px " id='address' style="display:none"><?php
                                                                                            if ($address) {
                                                                                                echo $address . "," . $city_name . "." . $pincode;
                                                                                            } else {
                                                                                                echo "-";
                                                                                            }
                                                                                            ?></p>
                                <p>
                                </p>
                                <p>
                                    <?php
                                    if ($state_name)
                                        echo $state_name . " , " . $country_name;
                                    else
                                        echo "-";
                                    ?>
                                </p>
                                <div>
                                    <span class="phone">
                                        <i class="material-icons">phone</i><?php echo "+" . $phone_code . " " . $seller_phone_no ?></span>
                                </div>
                                <div class="row">
                                    <div class="col-4">
                                        <h5>
                                            <?php
                                            $GetSellerDetailsUrl = $url.'api/EasyGift/Product?columns=Id&filter=ShopId='.$shop_id;
                                            $response = fetchRequest($GetSellerDetailsUrl);
                                            if($response['statusCode']==200)
                                            {
                                                echo count($response['result']);
                                            }
                                            else
                                            {
                                                echo 0;
                                            }
                                            ?>

                                        </h5>
                                        <a style="color: black" href="./product-list.php?shop_id=<?php echo $shop_id ?>">Total Products </a>
                                    </div>
                                    <div class=" col-4">
                                        <!-- <h5>
                                            <i class="fas fa-star" style="color:orange ; font-size:11pt ; margin: 0px"></i>
                                            <i class="fas fa-star" style="color:orange ; font-size:11pt ; margin: 0px"></i>
                                            <i class="fas fa-star" style="color:orange ; font-size:11pt ; margin: 0px"></i>
                                            <i class="fas fa-star" style="color:orange ; font-size:11pt ; margin: 0px"></i>
                                            <i class="fas fa-star" style="color:orange ; font-size:11pt ; margin: 0px"></i>
                                        </h5>
                                        <small>Rating</small> -->
                                    </div>
                                    <div class="col-4">
                                        <h5>
                                            <?php
                                            $GetSellerDetailsUrl = $url.'api/EasyGift/shop/GetTotalOrder/'.$shop_id;
                                            $response = fetchRequest($GetSellerDetailsUrl);
                                            if($response['statusCode']==200)
                                            {
                                              if(count($response['result'])>0)
                                              {
                                                echo $response['result'][0]['total_order'];
                                              }
                                            }
                                            ?>
                                        </h5>
                                        <a style="color: black" href="./order-list.php?shop_id=<?php echo $shop_id ?>">Total Orders </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <ul class="nav nav-tabs">
                            <li class="nav-item m-l-12">
                                <a class="nav-link active" data-toggle="tab" href="#about">Location</a>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <input type="hidden" name="latitude" id="latitude" value="<?php echo $latitude ?>">
                            <input type="hidden" name="longitude" id="longitude" value="<?php echo $longitude ?>">
                            <div class="tab-pane body active" id="map" style="height:300px">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-7 col-md-12">
                    <div class="card">
                        <div class="profile-tab-box">
                            <div class="p-l-20">
                                <ul class="nav ">
                                    <li class="nav-item tab-all">
                                        <a class="nav-link active show" id="About" href="#project" data-toggle="tab">About</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="project" aria-expanded="true">
                            <div class="row clearfix">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="card project_widget">
                                        <div class="header">
                                            <h2><strong>About</strong> Me</h2>
                                        </div>
                                        <div class="body">
                                            <div class="column">
                                                <div class="col-md-12">

                                                    <strong>Full Name</strong>
                                                    <br>
                                                    <p class="text-muted"><?php echo $seller_name . " " . $seller_l_name ?></p>
                                                </div>
                                                <div class="col-md-12">
                                                    <strong>Mobile</strong>
                                                    <br>
                                                    <p class="text-muted"><?php echo "+" . $phone_code . " " . $seller_phone_no ?></p>
                                                </div>
                                                <div class="col-md-12">
                                                    <strong>Email</strong>
                                                    <br>
                                                    <p class="text-muted"><?php echo $email_id ?></p>
                                                </div>
                                                <div class="col-md-12">
                                                    <strong>Pancard No</strong>
                                                    <br>
                                                    <p class="text-muted"><?php echo $seller_pancard_no ?></p>
                                                </div>

                                            </div>

                                            <br>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row clearfix">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="card project_widget">
                                        <div class="header">
                                            <h2><strong>About</strong> Shop</h2>
                                        </div>
                                        <div class="body">
                                            <div class="column">
                                                <div class="col-md-12">
                                                    <strong>Shop Name</strong>
                                                    <br>
                                                    <p class="text-muted"><?php
                                                                            if ($shop_name) {
                                                                                echo $shop_name;
                                                                            } else {
                                                                                echo "-";
                                                                            }
                                                                            ?></p>
                                                </div>
                                                <div class="col-md-12">
                                                    <strong>GST No</strong>
                                                    <br>
                                                    <p class="text-muted">
                                                        <?php
                                                        if ($gst_no) {
                                                            echo $gst_no;
                                                        } else {
                                                            echo "-";
                                                        } ?>
                                                    </p>
                                                </div>
                                                <div class="col-md-12">
                                                    <strong>Address</strong>
                                                    <br>
                                                    <p class="text-muted">
                                                        <?php
                                                        if ($address) {
                                                            echo $address . "," . $city_name . ".";
                                                        } else {
                                                            echo "-";
                                                        }
                                                        ?>
                                                    </p>
                                                </div>
                                                <div class="col-md-12">
                                                    <strong>Pincode</strong>
                                                    <br>
                                                    <p class="text-muted"><?php
                                                                            if ($pincode) {
                                                                                echo $pincode;
                                                                            } else {
                                                                                echo "-";
                                                                            } ?></p>
                                                </div>

                                                <div class="col-md-12">
                                                    <strong>State</strong>
                                                    <br>
                                                    <p class="text-muted"><?php
                                                                            if ($state_name) {
                                                                                echo $state_name;
                                                                            } else {
                                                                                echo "-";
                                                                            } ?></p>
                                                </div>
                                                <div class="col-md-12">
                                                    <strong>Country</strong>
                                                    <br>
                                                    <p class="text-muted">
                                                        <?php
                                                        if ($country_name)
                                                            echo $country_name;
                                                        else
                                                            echo "-";
                                                        ?>
                                                    </p>
                                                </div>
                                            </div>

                                            <br>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row clearfix">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="card project_widget">
                                        <div class="header">
                                            <h2><strong>Bank</strong> Details</h2>
                                        </div>
                                        <div class="body">
                                            <div class="column">
                                                <div class="col-md-12">
                                                    <strong>Bank Name</strong>
                                                    <br>
                                                    <p class="text-muted"><?php
                                                                            if ($bank_name)
                                                                                echo $bank_name;
                                                                            else
                                                                                echo "-";
                                                                            ?></p>
                                                </div>
                                                <div class="col-md-12">
                                                    <strong>Account no</strong>
                                                    <br>
                                                    <p class="text-muted">
                                                        <?php
                                                        if ($b_acc_no)
                                                            echo $b_acc_no;
                                                        else
                                                            echo "-";
                                                        ?>
                                                    </p>
                                                </div>
                                                <div class="col-md-12">
                                                    <strong>Branch Name</strong>
                                                    <br>
                                                    <p class="text-muted">
                                                        <?php
                                                        if ($bank_branch)
                                                            echo $bank_branch;
                                                        else
                                                            echo "-";
                                                        ?>
                                                    </p>
                                                </div>
                                                <div class="col-md-12">
                                                    <strong>Address</strong>
                                                    <br>
                                                    <p class="text-muted">
                                                        <?php
                                                        if ($bank_address)
                                                            echo $bank_address;
                                                        else
                                                            echo "-";
                                                        ?>
                                                    </p>
                                                </div>
                                                <div class="col-md-12">
                                                    <strong>Bank City</strong>
                                                    <br>
                                                    <p class="text-muted"><?php
                                                                            if ($bank_city)
                                                                                echo $bank_city;
                                                                            else
                                                                                echo "-";
                                                                            ?></p>
                                                </div>

                                                <div class="col-md-12">
                                                    <strong>State</strong>
                                                    <br>
                                                    <p class="text-muted"><?php
                                                                            if ($bank_state)
                                                                                echo $bank_state;
                                                                            else
                                                                                echo "-";
                                                                            ?></p>
                                                </div>
                                                <div class="col-md-12">
                                                    <strong>Country</strong>
                                                    <br>
                                                    <p class="text-muted">
                                                        <?php
                                                        if ($bank_country)
                                                            echo $bank_country;
                                                        else
                                                            echo "-";
                                                        ?>
                                                    </p>
                                                </div>
                                            </div>

                                            <br>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <script src="assets/js/app.min.js"></script>
    <!-- Custom Js -->
    <script src="assets/js/admin.js"></script>
    <script src="assets/js/custom.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAp5TcMKKv687gUTX3s0GJRqtt-KGjgMDo"></script>
    <!-- <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false"></script> -->
    <?php
    }
    else
    {
        echo "No Data Found";
    }
  }
?>
</body>


</html>
<?php 
ob_end_flush(); 
?>