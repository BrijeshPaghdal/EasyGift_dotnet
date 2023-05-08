<?php
    include 'check-seller.php';

    $output = "<select>";

    $cate_name = $_POST['cate_name'];

    $tempUrl = $url.'api/EasyGift/Category?filter=CategoryName.Equals("'.$cate_name.'")';
    $response = fetchRequest($tempUrl);
    if($response['statusCode']==200 && count($response['result'])>0)
    {
        $cate_id = $response['result'][0]['id'];
        echo $cate_id;
        $tempUrl = $url.'api/EasyGift/SubCategory?filter=CategoryId='.$cate_id.'&&Id<>0';
        $response = fetchRequest($tempUrl);
        if($response['statusCode']==200 && count($response['result'])>0)
        {
            $output .= "<select class='form-control select2' data-placeholder='Select' >";
            // $output .= "<option value=\"\" disabled selected>
            //                             Choose Product Sub Category
            //                         </option>";
            foreach ($response['result'] as $row) {
                $output .= "<option>".$row['subCategoryName']."</option>";
            }
        }
    }
    $output .= "</select>";
    echo $output;



?>
