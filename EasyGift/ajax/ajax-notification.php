<?php
session_start();
require_once('../config.php');

$user_id = $_SESSION['user_id'];

$tempUrl = $url.'api/EasyGift/Customer/'.$user_id.'/GetOrderCompleteNotification/';
$response = fetchRequest($tempUrl);
if ($response['statusCode']==200 && count($response['result']) > 0) {
  $output = "";
  foreach($response['result'] as $row) {
    $output .= '<div class="product">
            <div >
                <h4 class="product-title">
                    <b>' . $row["ProductName"] . '</b>
                </h4>
                <span class="cart-product-info">
                    Did You Receive this Item??
                </span>
                <div class="dropdown-cart-action">
                    <button class="btn btn-outline-primary-2" id="ord_comp" value = ' . $row["Id"] . '>Yes I did</button>
                    <button class="btn btn-outline-primary-2" id="ord_not_comp">No I didn\'t</button>
                </div>
            </div>
        </div>';
  }
} else {
  $output = "No notification Available";
}
echo $output;
