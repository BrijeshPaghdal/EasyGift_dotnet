<?php
    include 'check-seller.php';
    
    $output =' <select class="form-control custom-select" name="bank_city" id ="bank_city"> ';
    
    $state_name = $_POST['State'];

    $tempUrl = $url.'api/EasyGift/BankDetails/GetDistinctBankCities?BankState='.$state_name;
    $response = fetchRequest($tempUrl);
  
    if($response['statusCode']==200 && count($response['result'])>0)
    {
        $output .= "<option selected disabled value=''>Bank City</option>";
        foreach($response['result'] as $row)
        {
            $output .= "<option>" . $row['BankCity'] . "</option>";
        }
    }
    $output .= "</select>";
    echo $output;
    
?>