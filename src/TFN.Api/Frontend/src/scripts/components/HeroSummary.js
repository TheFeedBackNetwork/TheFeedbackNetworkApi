import React from 'react'
import { Row, Button } from 'react-bootstrap'

class HeroSummary extends React.Component {

    render() {
        return (
            <div>
                <Row>
                    <div className='col-3 links my-links'>
                        <Button className='btn-default-o'> Edit Profile </Button>
                    </div>
                </Row>
                <Row>
                <div className="stats">
                    <div className='stat'>
                        <div className='num' >
                            43
                        </div>
                        <div className='desc'>
                        uploaded
                        </div>
                    </div>
                    <div className='stat'>
                        <div className='num' >
                            1297
                        </div>
                        <div className='desc'>
                        listens
                        </div>
                    </div>
                </div>
                </Row>
            </div>
        )
    }
}

export default HeroSummary;