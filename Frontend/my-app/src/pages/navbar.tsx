
import React from 'react'

export default function navbar() {
  const user = JSON.parse(localStorage.getItem("user"));

  const decodedJwt = JSON.parse(window.atob(user.token.split(".")[1]));

  const logOut = (e: React.FormEvent<HTMLSelectElement>) => {
    if (e.currentTarget.value === "logout") {
      localStorage.removeItem("user");
      window.location.href = "/login";
    }
  }

  //Her 1 sn de token expire ı kontrol eder. Süresi dolmuşsa(30 dk) mevcut token ı silerek girişe yönlendirir
  setTimeout(function run() {
    if (decodedJwt.exp * 1000 < Date.now()) {
      localStorage.removeItem("user");
      window.location.href = "/login";
    }
    setTimeout(run, 1000);
  }, 1000);



  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-warning">
      <div className="container-fluid">
        <a className="navbar-brand" href="/home">AnaSayfa</a>
        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <a className="nav-link active" aria-current="page" href="/users">Kullanıcı Listesi</a>
            </li>
            <li className="nav-item">
              <a className="nav-link active" aria-current="page" href="/departments">Departman Listesi</a>
            </li>
          </ul>
          <ul>
            <select className="form-select" onChange={logOut}>
              <option selected> Aktif Kullanıcı: {user?.userName}</option>
              <option value="logout">Çıkış</option>
            </select>
          </ul>
        </div>
      </div>
    </nav>
  )
}
