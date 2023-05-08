<?php
include('../config.php');
$name = $_POST['register-name'];
$email = $_POST['register-email'];
$number = $_POST['register-number'];
$passwd = $_POST['register-password'];
$cpasswd = $_POST['register-confirm-password'];

// validation 
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
                case 2:
                    array_push($tempArray, "2");
                    break;
                case 3:
                    array_push($tempArray, "3");
                    break;
                case 4:
                    array_push($tempArray, "4");
                    break;
            }
        } else if ((strlen($val) !== 0)) {
            switch ($name) {
                case 0:
                    $t = checkLength($val, 3, 15);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }

                    break;
                case 1:
                    $t = checkEmail($val);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }
                    break;
                case 2:
                    $t = checkNumber($val);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }
                    break;
                case 3:
                    $t = checkPassword($val);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }
                    break;
                case 4:
                    $t = matchPassword($val);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }
                    break;
            }
        }
    }
    return $tempArray;
}

$temp = checkReqiredField([$name, $email, $number, $passwd, $cpasswd]);


function checkLength($input, $min, $max)
{
    if (strlen($input) < $min) {
        return 5;
    } else if (strlen($input) > $max) {
        return 5;
    }
}


function checkEmail($input)
{
    $vale = preg_match("/^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i", $input);
    if (!$vale) {
        return 6;
    }
}

function checkNumber($input)
{
    $val = preg_match("/^[+]?(\d{1,2})?[\s.-]?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/", $input);
    if (!$val) {
        return 7;
    }
}



function checkPassword($input)
{
    $val = preg_match("/^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/", $input);
    if (!$val) {
        return 8;
    }
}

function matchPassword($input)
{
    if ($input != $GLOBALS['passwd']) {
        return 9;
    }
}



if (count($temp) > 0) {
    echo json_encode($temp);
} else {

    $tempUrl = $url . 'api/EasyGift/CustomerLogin?filter=EmailId.Equals("'.$email.'")';
    $res1 = fetchRequest($tempUrl);
    $tempUrl = $url . 'api/EasyGift/Customer?filter=MobileNo.Equals("'.$number.'")';
    $res2 = fetchRequest($tempUrl);
    if ($res1['statusCode'] != 404 || $res2['statusCode'] != 404) {
        array_push($temp, "s");
        echo json_encode($temp);
    } else {
        $passwdmd = md5($passwd);

        $tempUrl = $url . 'api/EasyGift/CustomerLogin';
        $data = array('EmailId' => $email, 'Password' => $passwdmd);
        $data = json_encode($data);
        $response = addData($tempUrl,$data);
        if ($response['statusCode']==201 && count($response['result']) > 0 ) {
            $login_id = $response['result']['id'];
            $date = new DateTime();
            $dateString = $date->format('Y-m-d H:i:s');
            $tempUrl = $url . 'api/EasyGift/Customer';
            $data = array('CustomerName' => $name, 'MobileNo' => $number, 'CustomerLoginId'=>$login_id ,'CustomerStatus'=>1 , 'CreatedDate'=>$dateString);
            $data = json_encode($data);
            $response = addData($tempUrl,$data);
            if ($response['statusCode'] == 201 && count($response['result'])>0) {
                echo "success";
            }
        }
    }
}
