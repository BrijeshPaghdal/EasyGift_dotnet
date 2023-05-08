<?php
if (isset($_POST['catename'])) {

    require_once('./check-admin.php');

    $cate_name =  $_POST['catename'];
    $tempUrl = $url.'api/EasyGift/Category?filter=CategoryName.Contains("'.$cate_name.'")';
    $response = fetchRequest($tempUrl);
    if($response['statusCode']==404)
    {
        if (isset($_FILES['image']) && $_FILES['image']['name'] != '') {
            $file_name = $_FILES['image']['name'];
            $file_tmp_name = $_FILES['image']['tmp_name'];
            $extension = pathinfo($file_name, PATHINFO_EXTENSION);
            $valid_extension = array("jpg", "jpeg", "png");

            if (in_array($extension, $valid_extension)) {
                if ($_FILES['image']['size'] < 200000) {
                    $new_file_name = date("d-m-y-h-m-s") . "-" . rand(1, 10000) . $file_name;
                    move_uploaded_file($file_tmp_name, "../category-image/" . $new_file_name);

                    $data = array('CategoryName' => $cate_name,"CategoryImageName" => $new_file_name);
                    $data = json_encode($data);
                    $tempUrl = $url . 'api/EasyGift/Category/';
                    $response = addData($tempUrl,$data);
                    if ($response['statusCode'] == 201) {
                        echo "success";
                    } else{
                        $filePath = "../category-image/" . $new_file_name;
                        unlink($filePath);
                        echo "5";
                    }
                } else {
                    echo "1";
                }
            } else {
                echo "3";
            }
        } else {
            echo "2";
        }
    } else {
        echo "4";
    }
} else {
    header("Location:404.php");
}
