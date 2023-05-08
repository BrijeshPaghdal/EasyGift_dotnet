<?php
    include 'check-seller.php';
    
    $output = "<select>";
    
    $city_name = $_POST['City'];

    $tempUrl = $url.'api/EasyGift/BankDetails/GetDistinctBankNames?BankCity='.$city_name;
    $response = fetchRequest($tempUrl);
  
    if($response['statusCode']==200 && count($response['result'])>0)
    {
        $output .= "<option selected disabled value=''>Bank Name</option>";
        foreach($response['result'] as $row)
        {
            $output .= "<option>" . $row['BankName'] . "</option>";
        }
    }
    $output .= "</select>";
    echo $output;
?>