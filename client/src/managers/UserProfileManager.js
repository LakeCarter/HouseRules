const _apiUrl = "api/userProfile"

export const getUserProfiles = () => {
  return fetch(_apiUrl).then((res) => res.json())
}

export const getUserProfilesById =(userId)=>{
    return fetch(`${_apiUrl}/${userId}`).then((res)=> res.json())
}