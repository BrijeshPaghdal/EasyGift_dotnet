<?php
	session_start();
	include '../config.php';
	$session_id = session_id();
	$tempUrl = $url.'api/EasyGift/SellerOnline?filter=Session='.$session_id;
    $response = deleteRequest($tempUrl);
	session_destroy();
	header("Location:login.php");
	

?>
