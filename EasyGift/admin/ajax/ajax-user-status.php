<?php
require_once('./check-admin.php');
$user_id = $_POST['userid'];
$status =$_POST['stat'];
if ($status == 'Disable') {
    $val = 0;
} else {
    $val = 1;
}
$data = array('CustomerStatus' => $val);
$data = json_encode($data);
$UpdateCustomerStatusUrl = $url . 'api/EasyGift/Customer/'.$user_id;
$response = patchData($UpdateCustomerStatusUrl,$data);
if ($response['statusCode'] == 200) {
    echo "1";
} elseif ($response['statusCode'] == 404) {
    echo "0";
}
