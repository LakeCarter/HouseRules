import { useState,useEffect } from "react"
import { useParams } from "react-router-dom"
import { getUserProfilesById } from "../managers/UserProfileManager.js"
import { Card, CardBody, CardTitle } from "reactstrap"

export const UserProfileDetails = () => {
  const { userProfileId } = useParams()
  const [userProfile, setUserProfile] = useState({})

  useEffect(() => {
    getUserProfilesById(userProfileId).then(setUserProfile)
  }, [])

  return <div className="container">
    <h2>User Profile Details</h2>
    <Card color="dark" inverse>
        <CardBody>
            <CardTitle tag={"h4"}>{userProfile.userName}</CardTitle>
            <p>UserName: {userProfile.userName}</p>
            <p>Name: {userProfile.firstName} {userProfile?.lastName}</p>
        </CardBody>
    </Card>
  </div>
}
