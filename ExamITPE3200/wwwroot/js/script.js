// Get a reference to your div
const results = document.getElementById('results');
const filters = document.getElementById('filters');

// Function to check if the div has been scrolled down
function checkScroll() {
  const scrollTop = results.scrollTop;

  // Check if the div has been scrolled down (you can adjust the threshold)
  if (scrollTop > 10) {
    // Apply a box shadow or any other styling you want
    filters.style.boxShadow = '0px 23px 21px -22px rgba(0,0,0,0.75)';
  } else {
    // Remove the box shadow or reset styling when the div is at the top
    filters.style.boxShadow = 'none';
  }
}

// Attach the scroll event listener to your div
results.addEventListener('scroll', checkScroll);

// Call the checkScroll function on page load to handle initial scroll position
checkScroll();