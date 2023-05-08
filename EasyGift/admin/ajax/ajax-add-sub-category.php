<?php
if (isset($_POST['subcatename'])) {

    require_once('./check-admin.php');

    $cate_id = $_POST['Cate_Id'];
    $sub_c_name = $_POST['subcatename'];

    $tempUrl = $url.'api/EasyGift/SubCategory?columns=Id&filter=SubCategoryName.Contains("'.$sub_c_name.'")';
    $response = fetchRequest($tempUrl);
    if($response['statusCode']==404)
    {
        $data = array('SubCategoryName' => $sub_c_name,"CategoryId" => $cate_id);
        $data = json_encode($data);
        $tempUrl = $url . 'api/EasyGift/SubCategory/';
        $response = addData($tempUrl,$data);
        if ($response['statusCode'] == 201) {
              echo "success";
          } else {
              echo "2";
          }
    } else {
        echo "1";
    }
} else {
    header("Location:404.php");
}
