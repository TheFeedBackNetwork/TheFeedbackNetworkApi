import React from 'react'
import { Row } from 'react-bootstrap'
import loaderImg from 'images/bars-loader.svg'
import 'styles/loader.css'

class AppLoader extends React.Component {

    render() {
        
        return (
            <div className='app-loader'>
                <div className='loader'>
                    <img src={loaderImg}/>
                </div>
            </div>
        )

        /*return (
            <div className='app-loader'>
                <div className='loader'>
                    <img src={loaderImg}/>
                </div>
            </div>
        )*/
    }
}

export default AppLoader;