<?php
    require_once('./check-admin.php');
    require_once('../../config.php');

    $vendor_id =$_POST['vendorid'];
    $status =  $_POST['stat'];

    if ($status == 'Disable') {
        $val = 0;
    } else if ($status == 'Active') {
        $val = 1;
    } else if ($status == 'Confirmation') {
        $val = 2;
    } else if ($status == 'Reject') {
        $val = 3;
    }

    $data = array('SellerStatus' => $val);
	$data = json_encode($data);
    $UpdateSellerStatusUrl = $url . 'api/EasyGift/Seller/'.$vendor_id;
    $response = patchData($UpdateSellerStatusUrl,$data);
    if ($response['statusCode'] == 200) {
        echo "1";
    } elseif ($response['statusCode'] == 404) {
        echo "0";
    }