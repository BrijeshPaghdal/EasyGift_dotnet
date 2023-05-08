<?php
  include "../config.php";
  session_start();

  $cust_name = $_POST['cust_name'];
  $email_id = $_POST['email_id'];
  $mobile_no = $_POST['mobile_no'];
  $cust_id = $_SESSION['user_id'];

  $curr_passwd = $_POST['curr_passwd'];
  $hash_curr_passwd = md5($curr_passwd);
  $new_passwd = $_POST['new_passwd'];
  $hash_passwd = md5($new_passwd);

  $tempUrl = $url.'api/EasyGift/Customer?columns=CustomerLoginId&filter=Id=='.$cust_id;
  $response = fetchRequest($tempUrl);
  if ($response['statusCode'] == 200 && count($response['result'])>0) {
      foreach ($response['result'] as $row) {
          $cust_login_id = $row['CustomerLoginId'];
      }
  }

  if($cust_name == "" || $email_id == "" || $mobile_no == "" )
  {
    echo "0";
  }
  else if($cust_name != "" && $mobile_no != "" && $email_id != "")
  {
    $tempUrl = $url . 'api/EasyGift/Customer/'.$cust_id;
    $data = array('CustomerName' => $cust_name,
                  'MobileNo'=>$mobile_no);
    $data = json_encode($data);
    $response = patchData($tempUrl,$data);
    if($response['statusCode']==200 && count($response['result'])>0)
    {
      
      $tempUrl = $url . 'api/EasyGift/CustomerLogin/'.$cust_login_id;
      $data = array('EmailId' => $email_id);
      $data = json_encode($data);
      $response = patchData($tempUrl,$data);
      if($response['statusCode']==200 && count($response['result'])>0)
      {
        if($curr_passwd != "")
        {
          $tempUrl = $url . 'api/EasyGift/CustomerLogin/login';
          $data = array('EmailId' => $email_id, 'Password' => $hash_curr_passwd);
          $data = json_encode($data);
          $response = addData($tempUrl,$data);
          if($response['statusCode']==200 && count($response['result'])>0)
          {
            $tempUrl = $url . 'api/EasyGift/CustomerLogin/'.$cust_login_id;
            $data = array( 'password' => $hash_passwd);
            $data = json_encode($data);
            $response = patchData($tempUrl,$data);

            if($response['statusCode']==200 && count($response['result'])>0)
            {
              echo "2";
            }
          }
          else {
            echo "3";
          }
        }
        else {
          echo "1";
        }
      }
      else {
        echo "Data is not updated1";
      }
    }
    else {
      echo "Data is not updated";
    }
  }

?>
