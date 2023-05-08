<?php


// Connetion Include
require_once('../config.php');
// Sller Details
$seller_first_name = $_POST['register-first-name'];
$seller_last_name = $_POST['register-last-name'];
$seller_phone_no = $_POST['register-number'];
$seller_emailid = $_POST['register-email'];
$seller_passwd = $_POST['register-password'];
$seller_c_passwd = $_POST['register-confirm-password'];
$pancard_no = $_POST['register-vendor-pancard-no'];



function checkReqiredField($arrayInput)
{
    $tempArray = array();
    foreach ($arrayInput as $name => $val) {
        if (strlen($val) === 0) {
            switch ($name) {
                case 0:
                    array_push($tempArray, "a");
                    break;
                case 1:
                    array_push($tempArray, "b");
                    break;
                case 2:
                    array_push($tempArray, "c");
                    break;
                case 3:
                    array_push($tempArray, "d");
                    break;
                case 4:
                    array_push($tempArray, "e");
                    break;
                case 5:
                    array_push($tempArray, "f");
                    break;
                case 6:
                    array_push($tempArray, "g");
                    break;
            }
        } else if ((strlen($val) !== 0)) {
            switch ($name) {
                case 0:
                    $t = checkFirstNameLength($val, 3, 15);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }

                    break;
                case 1:
                    $t = checkLastNameLength($val, 3, 15);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }

                    break;
                case 2:
                    $t = checkEmail($val);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }
                    break;
                case 3:
                    $t = checkNumber($val);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }
                    break;
                case 4:
                    $t = checkPassword($val);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }
                    break;
                case 5:
                    $t = matchPassword($val);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }
                    break;

                case 6:
                    $t = checkPancard($val);
                    if ($t != "") {
                        array_push($tempArray, $t);
                    }
                    break;
            }
        }
    }
    return $tempArray;
}

// Check Required Field
$temp = checkReqiredField([
    $seller_first_name,
    $seller_last_name,
    $seller_emailid,
    $seller_phone_no,
    $seller_passwd,
    $seller_c_passwd,
    $pancard_no
]);

if ($temp == []) {
    $seller_passwd = md5($seller_passwd);
    // Check exists Already User
    $tempUrl = $url . 'api/EasyGift/SellerLogin?filter=EmailId.Equals("' . $seller_emailid . '")';
    $response = fetchRequest($tempUrl);
    if ($response['statusCode'] == 200 && count($response['result']) > 0) {
        echo "Email Already Exist";
    } else {
        if (isset($_FILES['image']) && $_FILES['image']['name'] != '') {
            $file_name = $_FILES['image']['name'];
            $file_tmp_name = $_FILES['image']['tmp_name'];
            $extension = pathinfo($file_name, PATHINFO_EXTENSION);
            $valid_extension = array("jpg", "jpeg", "png");

            if (in_array($extension, $valid_extension)) {
                if ($_FILES['image']['size'] < 500000) {
                    $new_file_name = date("d-m-y-h-m-s") . "-" . rand(1, 10000) . $file_name;
                    move_uploaded_file($file_tmp_name, "../vendor/seller-images/" . $new_file_name);

                    $data = array(
                        'SellerEmail' => $seller_emailid,
                        'SellerPassword' => $seller_passwd,
                        'SellerName' => $seller_first_name,
                        'SellerLastName' => $seller_last_name,
                        'SellerPhoneNo' => $seller_phone_no,
                        'SellerPancardNo' => $pancard_no,
                        'SellerImage' => $new_file_name
                    );
                    $data = json_encode($data);
                    $tempUrl = $url . 'api/EasyGift/Seller/CreateNewSeller';
                    $response = addData($tempUrl, $data);
                    if ($response['statusCode'] == 201 && count($response['result']) > 0) {
                        echo "success";
                    } else {
                        echo "something went wrong";
                    }
                } else {
                    echo "Image Size is too Big...";
                }
            } else {
                echo "Please Upload Proper Image";
            }
        } else {
            echo "Please Upload Image";
        }
    }

} else {
    echo json_encode($temp);
}

function checkFirstNameLength($input, $min, $max)
{
    if (strlen($input) < $min) {
        return 1;
    } else if (strlen($input) > $max) {
        return 1;
    }
}


function checkLastNameLength($input, $min, $max)
{
    if (strlen($input) < $min) {
        return 2;
    } else if (strlen($input) > $max) {
        return 2;
    }
}


function checkEmail($input)
{
    $vale = preg_match("/^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i", $input);
    if (!$vale) {
        return 3;
    }
}

function checkNumber($input)
{
    $val = preg_match("/^[+]?(\d{1,2})?[\s.-]?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/", $input);
    if (!$val) {
        return 4;
    }
}


function checkPassword($input)
{
    $val = preg_match("/^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/", $input);
    if (!$val) {
        return 5;
    }
}

// Check validate match password
function matchPassword($input)
{
    if ($input != $GLOBALS['seller_passwd']) {
        return 6;
    }
}

// Check validate pancard number
function checkPancard($input)
{
    $val = preg_match("/^([A-Z]{5})(\d{4})([A-Z]{1})$/", $input);
    if (!$val) {
        return 7;
    }
}