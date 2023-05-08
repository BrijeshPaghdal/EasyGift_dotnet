<?php
	include 'check-seller.php';

    $shop_id = $_SESSION['Shop_Id'];

	$id = $_POST['id'];

	$tempUrl = $url.'api/EasyGift/Shop/'.$shop_id.'/CompleteOrder/'.$id;
	$response = fetchRequest($tempUrl);

	if($response['statusCode']==200 && count($response['result'])>0)
	{
		$result = $response['result'][0]['Result'];
	}
	echo $result;
	
?>
