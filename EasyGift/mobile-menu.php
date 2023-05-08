  <?php require_once('./online.php'); ?>
  <!-- Mobile Menu -->
  <div class="mobile-menu-overlay"></div>
  <!-- End .mobil-menu-overlay -->

  <div class="mobile-menu-container">
      <div class="mobile-menu-wrapper">
          <span class="mobile-menu-close"><i class="icon-close"></i></span>

          <form action="#" method="get" class="mobile-search">
              <label for="mobile-search" class="sr-only">Search</label>
              <input type="search" class="form-control" name="mobile-search" id="mobile-search" placeholder="Search in..." required />
              <button class="btn btn-primary" type="submit">
                  <i class="icon-search"></i>
              </button>
          </form>

          <nav class="mobile-nav">
              <ul class="mobile-menu">
                  <?php
                    require_once("./config.php");
                    $tempUrl = $url . 'api/EasyGift/Category?filter=Id<>0&limit=6';
                    $response = fetchRequest($tempUrl);
                    if ($response['statusCode'] == 200 && count($response['result'])>0) {
                        foreach ($response['result'] as $row) {
                    ?>
                          <li>
                              <a href="#"><?php echo $row['categoryName'] ?></a>
                              <?php $cate_id = $row['id']; ?>
                              <ul>

                                  <li>
                                      <a>By Price</a>

                                      <ul>
                                          <li>
                                              <a href="filter-product.php?category=<?php echo $row['categoryName'] ?>&maxPrice=499">Under 499</a>
                                          </li>
                                          <li>
                                              <a href="filter-product.php?category=<?php echo $row['categoryName'] ?>&minPrice=500&maxPrice=599">500 To 599</a>
                                          </li>
                                          <li>
                                              <a href="filter-product.php?category=<?php echo $row['categoryName'] ?>&minPrice=600&maxPrice=999">600 To 999</a>
                                          </li>
                                          <li>
                                              <a href="filter-product.php?category=<?php echo $row['categoryName'] ?>&minPrice=1000&maxPrice=1999">1000 To 1999</a>
                                          </li>

                                          <li>
                                              <a href="filter-product.php?category=<?php echo $row['categoryName'] ?>&minPrice=2000">Above 2000</a>
                                          </li>

                                      </ul>
                                  </li>
                                  <li>
                                      <a>By Types</a>

                                      <ul>
                                          <?php
                                           $tempUrl = $url . 'api/EasyGift/SubCategory?filter=CategoryId=='.$cate_id.'&&Id<>0&limit=6';
                                           $response1 = fetchRequest($tempUrl);
                                           if ($response1['statusCode'] == 200 && count($response1['result'])>0) {
                                                   foreach ($response1['result'] as $row1) {
                                            ?>
                                                  <li>
                                                      <a href="filter-product.php?category=<?php echo $row['categoryName'] ?>&subcategory=<?php echo $row1['subCategoryName'] ?>"><?php echo $row1['subCategoryName'] ?></a>
                                                  </li>

                                          <?php }
                                            } ?>
                                      </ul>
                                  </li>
                              </ul>
                          </li>
                  <?php }
                    } ?>

              </ul>
          </nav>
          <!-- End .mobile-nav -->

          <div class="social-icons">
              <a href="#" class="social-icon" target="_blank" title="Facebook"><i class="icon-facebook-f"></i></a>
              <a href="#" class="social-icon" target="_blank" title="Twitter"><i class="icon-twitter"></i></a>
              <a href="#" class="social-icon" target="_blank" title="Instagram"><i class="icon-instagram"></i></a>
              <a href="#" class="social-icon" target="_blank" title="Youtube"><i class="icon-youtube"></i></a>
          </div>
          <!-- End .social-icons -->
      </div>
      <!-- End .mobile-menu-wrapper -->
  </div>
  <!-- End .mobile-menu-container -->