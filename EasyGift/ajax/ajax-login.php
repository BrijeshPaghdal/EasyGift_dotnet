<?php
include('../config.php');
$email = $_POST['singin-email'];
$passwd = $_POST['singin-password'];

function checkReqiredField($arrayInput)
{
    $tempArray = array();
    foreach ($arrayInput as $name => $val) {
        if (strlen($val) === 0) {
            switch ($name) {
                case 0:
                    array_push($tempArray, "0");
                    break;
                case 1:
                    array_push($tempArray, "1");
                    break;
            }
        } else if ((strlen($val) !== 0)) {
            switch ($name) {
                case 0:
                    $t = checkEmail($val);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }
                    break;
            }
        }
    }
    return $tempArray;
}

$temp = checkReqiredField([$email, $passwd]);

function checkEmail($input)
{
    $vale = preg_match("/^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i", $input);
    if (!$vale) {
        return 2;
    }
}





if (count($temp) > 0) {
    echo json_encode($temp);
} else {
    $md5passwd = md5($passwd);

    $loginurl = $url . 'api/EasyGift/CustomerLogin/login';
    $data = array('EmailId' => $email, 'Password' => $md5passwd);
    $data = json_encode($data);
    $response = addData($loginurl,$data);
    if($response['statusCode']==200 && count($response['result'])>0)
    {
        session_start();
        $cust_login_id = $response['result']['user']['id'];
        $tempUrl = $url . 'api/EasyGift/Customer?filter=CustomerLoginId=='.$cust_login_id;
        $response = fetchRequest($tempUrl);
        $_SESSION['user_id'] = $response['result'][0]['id'];
        if (isset($_POST['remember'])) {
            setcookie("user_login", $email, time() + (86400 * 5), "/");
            setcookie("user_password", $passwd, time() + (86400 * 5), "/");
        }
        echo "success";
    }
    else {
        array_push($temp, "3");
        echo json_encode($temp);
    }
}
