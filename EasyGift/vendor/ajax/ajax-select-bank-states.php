<?php
    include 'check-seller.php';
    
    $output = '<select <select class="form-control custom-select" name="bank_state" id ="bank_state">';
    
    $country_name = $_POST['Country'];

    $tempUrl = $url.'api/EasyGift/BankDetails/GetDistinctBankStates?BankCountry='.$country_name;
    $response = fetchRequest($tempUrl);
  
    if($response['statusCode']==200 && count($response['result'])>0)
    {
        $output .= "<option selected disabled value=''>Bank State</option>";
        foreach($response['result'] as $row)
        {
            $output .= "<option>" . $row['BankState'] . "</option>";
        }
    }
    $output .= "</select>";
    echo $output;
?>