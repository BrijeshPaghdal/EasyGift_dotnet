<?php
	include 'check-admin.php';

		date_default_timezone_set("Asia/Kolkata");
		$output = "";
		$temp_date = date('Y-d-m' , strtotime('-1 days'));
		$days = 7;
		$GetTotalOrderUrl = $url.'api/EasyGift/Order/GetPastOrder';
		$response = fetchRequest($GetTotalOrderUrl);
		if($response['statusCode']==200)
		{
			$output .= getLastDataForChart($response['result'],$days);
		}
		elseif($response['statusCode']==404)
		{
			echo 0;
		}
		$output .= "/";
		$GetTotalProductUrl = $url.'api/EasyGift/Product/GetPastAddedProduct';
		$response = fetchRequest($GetTotalProductUrl);
		$temp_date = date('Y-d-m' , strtotime('-1 days'));
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
