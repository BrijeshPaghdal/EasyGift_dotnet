<?php
header("Cache-Control: no-store, no-cache, must-revalidate, max-age=0");
header("Cache-Control: post-check=0, pre-check=0", false);
header("Pragma: no-cache");
	session_start();
	include '../../config.php';
	if(isset($_SESSION['Seller_Id']))
	{
		$seller_id = $_SESSION['Seller_Id'];
		$tempUrl = $url.'api/EasyGift/Seller?filter=Id='.$seller_id.'&& SellerStatus =1';
		$response = fetchRequest($tempUrl);
		
		if($response['statusCode'] == 200 && count($response['result'])>0)
		{
			foreach($response['result'] as $row)
			{
				if($row['sellerStatus'] == 0)
				{
					header("Location:../login.php");
				}
				else
				{
					return true;
				}
			}
		}
	}
	else
	{
		header("Location:../login.php");
	}
?>
