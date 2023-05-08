<?php

if (isset($_POST['S_Email'])) {
	include '../../config.php';

	$Email = $_POST['S_Email'];

	$Passwd = $_POST['S_Passwd'];

	$HashPasswd = md5($Passwd);
	
	$data = array('EmailId' => $Email, 'Password' => $HashPasswd);
	$data = json_encode($data);
	$tempUrl = $url.'api/EasyGift/SellerLogin/login';
	$response = addData($tempUrl,$data);
	if ($response['statusCode']==200 && count($response['result']) > 0) {
		
		$res = $response['result']['user'];
		$login_id = $res['id'];

		$tempUrl = $url.'api/EasyGift/Seller?filter=SellerLoginId='.$login_id;
		$response = fetchRequest($tempUrl);
		if ($response['statusCode']==200 && count($response['result']) > 0) {

			foreach($response['result'] as $row) {
				$status = $row['sellerStatus'];
				$sellerName = $row['sellerName'];
				$seller_id = $row['id'];
				$seller_image = $row['sellerImage'];
			}
			if ($status == 1) {
				session_start();
				$tempUrl = $url.'api/EasyGift/Shop?filter=SellerId='.$seller_id;
				$response = fetchRequest($tempUrl);
				if ($response['statusCode']==200 && count($response['result']) > 0) {
					
					foreach($response['result'] as $row) {
						$shop_id = $row['id'];
					}
				}
				if (isset($_POST['remember'])) {
					setcookie("vendor_login", $Email, time() + (86400 * 5), "/");
					setcookie("vendor_password", $Passwd, time() + (86400 * 5), "/");
				}
				$_SESSION['Seller_Name'] = $sellerName;
				$_SESSION['Seller_Id'] = $seller_id;
				$_SESSION['Shop_Id'] = $shop_id;
				$_SESSION['Seller_Image'] = $seller_image;
				// echo $_SESSION['Seller_Name']."<br>".
				// 			$_SESSION['Seller_Id']."<br>".
				// 			$_SESSION['Shop_Id']."<br>".
				// 			$_SESSION['Seller_Image']."<br>"; 
				echo 1;
			} else if ($status == 2) {
				echo 2;
			} else if ($status == 0) {
				echo 0;
			} else if ($status == 3) {
				echo 3;
			}
		}
	} else {
		echo "4";
	}
} else {
	header("Location:../login.php");
}
