<?php
	include 'check-admin.php';

	date_default_timezone_set("Asia/Kolkata");
		$output = "";
		$temp_date = date('Y-d-m' , strtotime('-1 days'));
		$days = 30;
		$GetNewSellerUrl = $url.'api/EasyGift/Seller/GetRecentNewSeller';
		$response = fetchRequest($GetNewSellerUrl);
		if($response['statusCode']==200)
		{
			$output .= getLastDataForChart($response['result'],$days);
		}
		elseif($response['statusCode']==404)
		{
			echo 0;
		}
		$output .= "/";
		$GetNewCustomerUrl = $url.'api/EasyGift/Customer/GetRecentNewCustomer';
		$response = fetchRequest($GetNewCustomerUrl);
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
