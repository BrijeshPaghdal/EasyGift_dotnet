<?php
$title = "Product Detail";
require_once('check-admin.php');
$prod_id = $_GET['prod_id'];

$tempUrl = $url.'api/EasyGift/Product/GetProductDetail/'.$prod_id;
$response = fetchRequest($tempUrl);
if ($response['statusCode']==200 && count($response['result']) > 0) {

    $row = $response['result'];

    $shop_id = $row['ShopId'];
    $shop_name = $row['ShopName'];
    $cate_name = $row['CategoryName'];
    $sub_c_name = $row['SubCategoryName'];
    $prod_name = $row['ProductName'];
    $comp_name = $row['CompanyName'];
    $price = $row['Price'];
    $avai_qua = $row['AvailableQuantity'];
    $prod_desc = $row['ProductDiscription'];
    $prod_status = $row['ProductStatus'];
    $total_order = $row['TotalOrder'];
    $add_date = $row['CreatedDate'];
    $add_date = new DateTime($add_date);
    $add_date = $add_date->format('d-m-Y');
    $update_date = $row['UpdateDate'];
    $update_date = new DateTime($update_date);
    $update_date = $update_date->format('d-m-Y');
    include 'header.php';
?>
    <section class="content">
        <div class="container-fluid">
            <div class="block-header">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <ul class="breadcrumb breadcrumb-style ">
                            <li class="breadcrumb-item">
                                <h4 class="page-title">Product Details</h4>
                            </li>
                            <li class="breadcrumb-item bcrumb-1">
                                <a href="index.php">
                                    <i class="fas fa-home"></i> Home</a>
                            </li>
                            <li class="breadcrumb-item bcrumb-2">
                              <a href="product-list.php?shop_id=<?php echo $shop_id?>">Products</a>
                            </li>
                            <li class="breadcrumb-item active">Product Details</li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card">
                        <div class="body">
                            <div class="block-header">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <ul class="breadcrumb breadcrumb-style ">
                                            <li class="bcrumb-1">
                                                <a href="./index.php">Home</a>
                                            </li>
                                            <li class="bcrumb-2">
                                                <a href="product-list.php?shop_id=<?php echo $shop_id?>">Products</a>
                                            </li>
                                            <li class="bcrumb-3">
                                                <a href="#" onClick="return false;"><?php echo $prod_name ?></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body ">
                                <div class="product-store">
                                    <div class="row">
                                        <div class="col-xl-6 col-lg-6 col-md-12 col-sm-12">
                                            <div class="product-gallery">
                                                <div class="product-gallery-thumbnails">
                                                    <ol class="thumbnails-list list-unstyled">
                                                        <?php

                                                        $tempUrl = $url.'api/EasyGift/Image?filter=ProductId='.$prod_id;
                                                        $res = fetchRequest($tempUrl);
                                                        $image_name = "";
                                                        if ($res['statusCode'] == 200 && count($res['result'])>0) {
                                                        $tmp = 0;
                                                        foreach ($res['result'] as $row1) {
                                                            if($tmp == 0)
                                                                $image_name = $row1['imageName'];
                                                            $tmp++;
                                                            echo "<li><img src='../vendor/product-images/{$row1['imageName']}'></li>";
                                                        }
                                                        }
                                                        else
                                                        {
                                                        $image_name = "";
                                                        }
                                                        ?>

                                                    </ol>
                                                </div>
                                                <div class="product-gallery-featured">
                                                    <img src='../vendor/product-images/<?php echo $image_name ?>'>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-6 col-lg-6 col-md-12 col-sm-12">
                                            <div class="product-payment-details">
                                                <p class="last-sold text-muted">
                                                <h6 style="opacity: 0.6">
                                                    Sold Count : <?php echo $total_order ?>
                                                </h6>
                                                </p>
                                                <h4 class="product-title mb-2">
                                                    <?php echo $prod_name ?>
                                                </h4>
                                                <h2 class="product-price display-4">
                                                    ₹ <?php echo $price ?>
                                                </h2>
                                                <p>
                                                    <strong>
                                                        <?php echo $prod_desc ?>
                                                    </strong>
                                                </p>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <input type="hidden" id='prod_id' name="prod_id" value="<?php echo $prod_id; ?>">
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card">
                        <div class="body">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs tab-nav-right" role="tablist">
                                <li role="presentation">
                                    <a href="#home" data-toggle="tab" class="active show">Product Details</a>
                                </li>
                                <li role="presentation">
                                    <a href="#profile" data-toggle="tab">Suggested For</a>
                                </li>
                                <li role="presentation">
                                    <a href="#reviews" data-toggle="tab">Reviews</a>
                                </li>
                            </ul>
                            <!-- Tab panes -->
                            <div class="tab-content">
                                <div role="tabpanel" class="tab-pane fade in active show" id="home">
                                    <div class="product-description">
                                        <h2 class="mb-5">Product Detail</h2>
                                        <dl class="row mb-5">
                                            <dt class="col-sm-3">Category Name</dt>
                                            <dd class="col-sm-9">
                                                <?php echo $cate_name ?>
                                            </dd>
                                            <dt class="col-sm-3">Sub Category Name</dt>
                                            <dd class="col-sm-9">
                                                <?php echo $sub_c_name ?>
                                            </dd>
                                            <dt class="col-sm-3">Company Name</dt>
                                            <dd class="col-sm-9">
                                                <?php echo $comp_name ?>
                                            </dd>
                                            <dt class="col-sm-3">Price</dt>
                                            <dd class="col-sm-9">
                                                <?php echo $price ?>
                                            </dd>
                                            <dt class="col-sm-3">Available Quantity</dt>
                                            <dd class="col-sm-9">
                                                <?php echo $avai_qua ?>
                                            </dd>
                                            <dt class="col-sm-3">Total Sell</dt>
                                            <dd class="col-sm-9">
                                                <?php echo $total_order ?>
                                            </dd>
                                            <dt class="col-sm-3">Status</dt>
                                            <dd class="col-sm-9">
                                                <?php
                                                if ($prod_status == 1)
                                                    echo "<span class='label bg-green shadow-style'>Active</span>";
                                                else
                                                    echo "<span class='label bg-red shadow-style'>Disable</span>";
                                                ?>
                                            </dd>
                                            <dt class="col-sm-3">Add Date</dt>
                                            <dd class="col-sm-9">
                                                <?php echo $add_date ?>
                                            </dd>
                                            <dt class="col-sm-3">Update Date</dt>
                                            <dd class="col-sm-9">
                                                <?php
                                                if ($update_date == '')
                                                    echo "-";
                                                else
                                                    echo $update_date;
                                                ?>
                                            </dd>
                                        </dl>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane fade" id="profile">
                                    <div class="product-description">
                                        <h2 class="mb-5">Suggested For</h2>
                                        <?php

                                        $tempUrl = $url.'api/EasyGift/ProductSuggestion/GetProductSuggestions/'.$prod_id;
                                        $res = fetchRequest($tempUrl);
                                        if ($res['statusCode'] == 200 && count($res['result'])>0) {
                                            foreach($res['result'] as $row) {
                                                echo "<dt class='col-sm-3'>{$row['SuggestedFor']}</dt>";
                                            }
                                        }
                                        ?>
                                    </div>
                                    <div role="tabpanel" class="tab-pane fade" id="shop">
                                        <div class="product-description">
                                            <h2 class="mb-5">Shop Details</h2>
                                            <dl class="row mb-5">
                                                <dt class="col-sm-3">Category Name</dt>
                                                <dd class="col-sm-9">
                                                    <?php echo $cate_name ?>
                                                </dd>
                                                <dt class="col-sm-3">Sub Category Name</dt>
                                                <dd class="col-sm-9">
                                                    <?php echo $sub_c_name ?>
                                                </dd>
                                                <dt class="col-sm-3">Company Name</dt>
                                                <dd class="col-sm-9">
                                                    <?php echo $comp_name ?>
                                                </dd>
                                                <dt class="col-sm-3">Price</dt>
                                                <dd class="col-sm-9">
                                                    <?php echo $price ?>
                                                </dd>
                                                <dt class="col-sm-3">Available Quantity</dt>
                                                <dd class="col-sm-9">
                                                    <?php echo $avai_qua ?>
                                                </dd>
                                                <dt class="col-sm-3">Total Sell</dt>
                                                <dd class="col-sm-9">
                                                    <?php echo $total_order ?>
                                                </dd>
                                                <dt class="col-sm-3">Status</dt>
                                                <dd class="col-sm-9">
                                                    <?php
                                                    if ($prod_status == 1)
                                                        echo "<span class='label bg-green shadow-style'>Active</span>";
                                                    else
                                                        echo "<span class='label bg-red shadow-style'>Disable</span>";
                                                    ?>
                                                </dd>
                                                <dt class="col-sm-3">Add Date</dt>
                                                <dd class="col-sm-9">
                                                    <?php echo $add_date ?>
                                                </dd>
                                                <dt class="col-sm-3">Update Date</dt>
                                                <dd class="col-sm-9">
                                                    <?php
                                                    if ($update_date == '')
                                                        echo "-";
                                                    else
                                                        echo $update_date;
                                                    ?>
                                                </dd>
                                            </dl>
                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane fade" id="reviews">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </section>
<?php
} else {
    header("Location:404.php");
}
?>
<script src="assets/js/app.min.js"></script>
<!-- Custom Js -->
<script src="assets/js/admin.js"></script>
<script src="assets/js/pages/ecommerce/product-detail.js"></script>
<script src="assets/js/custom.js"></script>

</body>

</html>
