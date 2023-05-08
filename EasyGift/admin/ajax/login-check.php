<?php
if (isset($_POST['A_Email'])) {
	include '../../config.php';

	$Email = $_POST['A_Email'];

	$Passwd = $_POST['A_Passwd'];

	$loginurl = $url . 'api/EasyGift/Admin/login';
	$data = array('EmailId' => $Email, 'Password' => $Passwd);
	$data = json_encode($data);
	// $url = $url.'?filter=AdminEmail="'.$Email.'"&&AdminPassword="'.$Passwd.'"';

	if (isset($_POST['remember'])) {
		setcookie("admin_login", $Email, time() + (86400 * 5), "/");
		setcookie("admin_passwd", $Passwd, time() + (86400 * 5), "/");
	}
	$options = array(
		CURLOPT_RETURNTRANSFER => true,												// Return response as a string
		CURLOPT_CUSTOMREQUEST => 'POST',														
		CURLOPT_POSTFIELDS => $data,												// Encode data as JSON and send in the request body
		CURLOPT_HTTPHEADER => array('Content-Type: application/json-patch+json'), 	// Set content type to JSON
		CURLOPT_SSL_VERIFYHOST =>false,
		CURLOPT_SSL_VERIFYPEER => false
	);
	
	$ch = curl_init($loginurl);
	curl_setopt_array($ch, $options);
	$response = curl_exec($ch);
	if($err = curl_error($ch))
	{
		echo $err;
	}

	curl_close($ch);
	$json = json_decode($response,true);
	// Display the response
	if($json['statusCode'] == 200)
	{
		session_start();
		$_SESSION['Admin_Id'] = $json['result']['user']['id'];
		$_SESSION['Admin_Name'] = $json['result']['user']['adminName'];
		$_SESSION['Admin_Emailid'] = $json['result']['user']['adminEmail'];

		echo 1;
	} else {
		print_r($json);
	}
	

} else {
	header('Location:404.php');
}