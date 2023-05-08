<?php
  $title = "Add Product";
  require_once("./header.php");
?>
<section class="content">
  <div class="container-fluid">
    <div class="block-header">
      <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
          <ul class="breadcrumb breadcrumb-style">
            <li class="breadcrumb-item">
              <h4 class="page-title"><?php echo $title ?></h4>
            </li>
            <li class="breadcrumb-item bcrumb-1">
              <a href="./index.php">
                <i class="fas fa-home"></i> Home</a>
            </li>
            <li class="breadcrumb-item bcrumb-2">
              <a href="./product-list.php">Product</a>
            </li>
            <li class="breadcrumb-item active"><?php echo $title ?></li>
          </ul>
        </div>
      </div>
    </div>
    <!-- Add Product -->
    <div class="row clearfix">
      <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="card">
          <div class="header">
            <h2><strong>Add</strong> Product</h2>
          </div>
          <div class="body">
            <form id="formAddProduct" method="POST">
              <div class="form-group form-float">
                <div class="form-line">
                  <input type="text" class="form-control" name="Prod_Name" maxlength="50" minlength="3" required />

                  <label class="form-label">Product Name</label>
                </div>
                <div class="help-info">Min. 3, Max. 50 characters</div>
              </div>
              <div class="form-group form-float">
                <div class="form-line">
                  <input type="text" class="form-control" name="Prod_Company" maxlength="30" minlength="3" required />

                  <label class="form-label">Product Company Name</label>
                </div>
                <div class="help-info">Min. 3, Max. 30 characters</div>
              </div>
              <div class="form-group default-select select2Style">
                <select name = "Cate_Name" id = "Cate_Name" class="form-control select2" >

                    <?php
                      $tempUrl = $url.'api/EasyGift/Category?filter=Id<>0';
                      $response = fetchRequest($tempUrl);
                      if($response['statusCode']==200 && count($response['result']) > 0)
                      {
                          foreach($response['result'] as $row)
                          {
                              $cate_id_2 = $row['id'];

                              $tempUrl = $url.'api/EasyGift/SubCategory?filter=CategoryId='.$cate_id_2;
                              $response = fetchRequest($tempUrl);
                              if($response['statusCode']==200 && count($response['result']) > 0)
                              {
                                  echo "<option>".$row['categoryName']."</option>";
                              }
                          }
                      }
                    ?>
                </select>
              </div>

                <div class="form-group default-select select2Style">
                    <select class="form-control select2" data-placeholder="Select" name="Sub_C_Name" id="Sub_C_Name">
                        <option value="" disabled selected>
                            Choose Product Sub Category
                        </option>
                    </select>
                </div>

              <div class="form-group form-float">
                <div class="form-line">
                  <input type="number" class="form-control" name="Price" maxlength="8" minlength="1" min="1" required />
                  <label class="form-label">Product Price</label>
                </div>
                <div class="help-info">Price only</div>
              </div>

              <div class="form-group form-float">
                <div class="form-line">
                  <input type="number" class="form-control" name="Avail_Qua" maxlength="8" minlength="1" min="0" required />

                  <label class="form-label">Product Quantity</label>
                </div>
                <div class="help-info"></div>
              </div>

              <div class="form-group form-float">
                <div class="form-line">
                  <textarea name="Prod_Description" cols="30" rows="5" class="form-control no-resize" required></textarea>
                  <label class="form-label">Product Description</label>
                </div>
              </div>

              <div class="form-group">
                <h2 class="card-inside-title">Product Suggest For</h2>
                <?php
                     $tempUrl = $url.'api/EasyGift/Suggestion';
                     $response = fetchRequest($tempUrl);
                     if($response['statusCode']==200 && count($response['result'])>0)
                     {
                        $temp=0;
                        $checked = "";
                        foreach ($response['result'] as $row) {
                            if($temp==0)
                            {
                                $checked = "checked";
                                $temp++;
                            }
                            else
                            {
                                $checked = "";
                            }
                            echo "<label style=''>
                                        <input class='with-gap' type='checkbox' name = 'suggest_for[]' value = '{$row['id']}'  $checked/>
                                        <span style='padding-left:25px;padding-right:20px'>{$row['suggestedFor']}</span>
                                </label>";
                        }
                    }
                ?>
                </div>
              <div class="form-group form-float">
                <h2 class="card-inside-title">Image Upload</h2>
                <div class="file-field input-field">
                  <div class="btn">
                    <span>Image</span>
                    <input type="file" name = "image[]" multiple />
                  </div>
                  <div class="file-path-wrapper">
                    <input class="file-path validate" type="text" placeholder="Upload one or more Image" />
                  </div>
                </div>
              </div>

              <button class="btn btn-primary waves-effect" type="submit">
                SUBMIT
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
    <!-- #END# Vertical Layout -->
  </div>
</section>
<!-- Plugins Js -->
<script src="assets/js/bundles/multiselect/js/jquery.multi-select.js"></script>
<script src="assets/js/app.min.js"></script>
<script src="assets/js/form.min.js"></script>
<script src="assets/js/bundles/bootstrap-colorpicker/dist/js/bootstrap-colorpicker.js"></script>

<!-- Custom Js -->
<script src="assets/js/admin.js"></script>
<script src="assets/js/custom.js"></script>
<script src="assets/js/pages/forms/form-validation.js"></script>
<script src="assets/js/pages/forms/advanced-form-elements.js"></script>
<script src="assets/js/pages/forms/basic-form-elements.js"></script>

<!-- Demo Js -->
</body>

</html>
