<?php
require_once('./check-admin.php');
$prod_id = $_POST['prodid'];
$status = $_POST['stat'];
if ($status == 'Disable') {
    $val = 0;
} else if ($status == 'Enable') {
    $val = 1;
}
$data = array('ProductStatus' => $val);
$data = json_encode($data);
$tempUrl = $url . 'api/EasyGift/Product/'.$prod_id;
$response = patchData($tempUrl,$data);
if ($response['statusCode'] == 200) {
    echo "1";
} elseif ($response['statusCode'] == 404) {
    echo "2";
}
