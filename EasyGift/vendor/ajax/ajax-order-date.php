<?php
	include 'check-seller.php';
	$shop_id = $_SESSION['Shop_Id'];
	date_default_timezone_set("Asia/Kolkata");
  $days = 7;
  $GetTotalOrderUrl = $url.'api/EasyGift/Order/GetPastOrder?ShopId='.$shop_id.'&limit='.$days;
  $response = fetchRequest($GetTotalOrderUrl);
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
