<?php
	include '../config.php';
	$date = date_default_timezone_set('Asia/Kolkata');

	if(isset($_POST['changePasswd']))
	{
		$id = $_POST['id'];
		$passwd = $_POST['new_passwd'];
		$tempUrl = $url.'api/EasyGift/SellerForgotPassword';
		$response = fetchRequest($tempUrl);
		if ($response['statusCode'] == 200 && count($response['result'])>0) {
			foreach ($response['result'] as $row) {
				$s_login_id = $row['sellerLoginId'];
				$s_login_id_hash = md5($s_login_id);

				if($id === $s_login_id_hash)
				{
					$temp = 1;
					break;
				}
				else {
					$temp = 0;
				}
			}
			if($temp == 0)
			{
				header("Location:404.php");
			}
			else {
				$passwdmd = md5($passwd);
				$tempUrl = $url . 'api/EasyGift/SellerLogin/'.$s_login_id;
				$data = array( 'password' => $passwdmd);
				$data = json_encode($data);
				$response = patchData($tempUrl,$data);
				if($response['statusCode']==200 && count($response['result'])>0)
				{
					$tempUrl = $url.'api/EasyGift/SellerForgotPassword?filter=SellerLoginId=='.$s_login_id;
					$response = deleteRequest($tempUrl);
					if ($response['statusCode'] == 200) {
						header("Location:login.php?msg=PasswordChanged");
					}
				}
			}
		}
	}

?>
<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8" />
  <meta content="width=device-width, initial-scale=1" name="viewport" />
  <title>Easygift</title>
  <!-- Favicon-->
  <link rel="icon" href="assets/images/favicon.ico" type="image/x-icon" />

  <!-- Plugins Core Css -->
  <link href="assets/css/app.min.css" rel="stylesheet" />

  <!-- Custom Css -->
  <link href="assets/css/style.css" rel="stylesheet" />
  <link href="assets/css/pages/extra_pages.css" rel="stylesheet" />
</head>
<?php
if(isset($_GET['id']))
{
		$id = $_GET['id'];

		$tempUrl = $url . 'api/EasyGift/SellerForgotPassword';
		$response = fetchRequest($tempUrl);
		if ($response['statusCode'] == 200 && count($response['result']) > 0) {
			foreach ($response['result'] as $row) {
				$s_login_id = $row['sellerLoginId'];
				$valid_till = $row['validtill'];
				$valid_till = DateTime::createFromFormat('Y-m-d\TH:i:s', $valid_till);
				$valid_till=$valid_till->format('Y-m-d H:i:s');
				$s_login_id_hash = md5($s_login_id);

				$currTime = date('Y-m-d H:i:s');
				if ($id === $s_login_id_hash && $valid_till >= $currTime) {
					$temp = 1;
					break;
				} else {
					$temp = 0;
				}
			
			}
			if ($temp == 0) {
				echo "<script> window.location = '404.php';</script>";
			}
?>
<body class="login-page">
  <div class="limiter">
    <div class="container-login100 page-background">
      <div class="wrap-login100">
        <form class="login100-form validate-form" method="post" id="forgorPasswd" action="change-passwd.php">
          <span class="login100-form-logo">
            <img alt="" src="assets/images/loading.png" />
          </span>
          <span class="login100-form-title p-b-34 p-t-27"> Change Password </span>
          <div class="wrap-input100 validate-input" data-validate="Enter username">
            <input class="input100" type="password" name = "new_passwd" id="new_passwd" placeholder="New Password" maxlength="30" minlength="3"
            value="" />
            <i class="material-icons focus-input1001">lock</i>
          </div>
          <div class="wrap-input100 validate-input" data-validate="Enter password">
            <input class="input100" type="password" id="cnf_passwd" name="cnf_passwd" placeholder="Confirm Password" value="" />
            <i class=" material-icons focus-input1001">lock</i>
          </div>

          <div class="container-login100-form-btn">
						<input type="hidden" name="id" value="<?php echo $id?>">
            <button class="login100-form-btn" name="changePasswd">Change Password</button>
          </div>
        </form>
      </div>
    </div>
  </div>

  <!-- Plugins Js -->

  <script src="assets/js/app.min.js"></script>

  <!-- Extra page Js -->
  <script src="assets/js/pages/examples/pages.js"></script>

  <!-- Custom JS -->
  <script src="assets/js/custom.js"></script>
</body>

<?php
	}
	else {
		header("Location:404.php");
	}
}
?>
</html>
