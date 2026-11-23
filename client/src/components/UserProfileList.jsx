import { useEffect, useState } from "react"
import { getUserProfiles } from "../managers/UserProfileManager.js"
import {
  Button,
  Card,
  CardBody,
  CardSubtitle,
  CardText,
  CardTitle,
} from "reactstrap"
import { useNavigate } from "react-router-dom"

export const UserProfileList = () => {
  const [users, setUsers] = useState([])

  const navigate = useNavigate()

  useEffect(() => {
    getUserProfiles().then(setUsers)
  }, [])

  return (
    <div className="container">
      <h2>User Profile List</h2>
      <div className="row">
        {users.map((user) => {
          return (
            <Card
              color="dark"
              outline
              style={{ marginBottom: "4px" }}
              key={user.id}
            >
              <CardBody>
                <CardTitle tag={"h5"}>{user.username}</CardTitle>
                <CardSubtitle className="mb-2 text-muted" tag="h6">
                  Name: {user.firstName} {user.lastName}
                </CardSubtitle>
                <CardText>Username: {user.userName}</CardText>
                <Button
                  onClick={() => {
                    navigate(`/userProfiles/details/${user.id}`)
                  }}
                >
                  Show Details
                </Button>
              </CardBody>
            </Card>
          )
        })}
      </div>
    </div>
  )
}
