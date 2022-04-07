let searchBtn = document.getElementById('search-article-btn');

searchBtn.addEventListener("click", searchArticles);


function searchArticles() {
    let searchTxt = document.getElementById('search-article-text').value;

    console.log(searchTxt);

    let articlesList = document.getElementById('articles-list');
    articlesList.innerHTML = '';

    let spinner = document.getElementById('spinner');
    spinner.removeAttribute('hidden');

    let data = { searchText: searchTxt };

    fetch('/articles/search',
            {
                method: 'POST', // *GET, POST, PUT, DELETE, etc.
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
        .then((response) => {
            if (response.ok) {
                return response.text();
                //return response.json();
            } else {
                console.log(response.status);
                return null;
            }
        })
        .then((html) => {
            articlesList.innerHTML = html;
            spinner.addAttribute('hidden');
        });
};
        //.then((json) => {
        //   return;
        //})};
   
