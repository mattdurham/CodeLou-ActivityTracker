var loginBtn = document.getElementById('getData');

loginBtn.addEventListener('click', function(e) {
  $.ajax({
    url: '/people/private',
    type: 'GET',
    async: true,
    contentType: 'application/x-www-urlencoded',
    dataType: 'json',
    beforeSend: function(xhr) {
      xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('access_token'));
    },
    error: function(xhr, status, error) {
      console.log(xhr);
    },
    success: function(xhr, status, error) {
      console.log(xhr);
    },
    cache: true
  });
});
