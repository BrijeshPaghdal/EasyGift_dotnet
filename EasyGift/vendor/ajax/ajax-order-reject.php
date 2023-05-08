<?php
	include 'check-seller.php';

	$order_id =$_POST['order_id'];

	$reject_status = 2;
	$data = array('OrderId' => $order_id,"OrderCompleteStatus" => $reject_status);
	$data = json_encode($data);
	$tempUrl = $url . 'api/EasyGift/OrderComplete';
	$response = addData($tempUrl,$data);
	if ($response['statusCode'] == 201) {
		$data = array('Status' => $reject_status);
		$data = json_encode($data);
		$tempUrl = $url . 'api/EasyGift/Order/'.$order_id;
		$response = patchData($tempUrl,$data);
		if ($response['statusCode'] == 200) {
			echo "1";
		} else{
			echo "2";
		}
	} else {
		echo "2";
	}
?>
