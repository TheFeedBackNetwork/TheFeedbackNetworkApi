import React , { PropTypes } from 'react'

class ActivityLogContainer extends React.Component {


    render() {
        return(
            <div>
                <p className="log"><span className="stamp">13 mins ago <span className="username">&lt;Screamo2&gt;:</span></span> Really good tunes, want to hear some more of that in next album! <a href="#">@2.27</a> was amazing</p>
                    
                    <p className="log"><span className="stamp">2 hours ago <span className="username">&lt;Julio880&gt;:</span></span> Awesome I like, one of my fav</p>
                    
                    <p className="log"><span className="stamp">8 hours ago <span className="username">&lt;Dart011&gt;:</span></span> Love the <a href="#">2:28</a> part, tune is great</p>
                    
                    <p className="log"><span className="stamp">13 mins ago <span className="username">&lt;Screamo2&gt;:</span></span> Really good tunes, want to hear some more of that in next album! <a href="#">@2.27</a> was amazing</p>
                    <p className="log"><span className="stamp">13 mins ago <span className="username">&lt;Screamo2&gt;:</span></span> Really good tunes, want to hear some more of that in next album! <a href="#">@2.27</a> was amazing</p>

                    <p className="log"><span className="stamp">2 hours ago <span className="username">&lt;Julio880&gt;:</span></span> Awesome I like, one of my fav</p>

                    <p className="log"><span className="stamp">8 hours ago <span className="username">&lt;Dart011&gt;:</span></span> Love the <a href="#">2:28</a> part, tune is great</p>

                    <p className="log"><span className="stamp">13 mins ago <span className="username">&lt;Screamo2&gt;:</span></span> Really good tunes, want to hear some more of that in next album! <a href="#">@2.27</a> was amazing</p>

                    <p className="log"><span className="stamp">2 hours ago <span className="username">&lt;Julio880&gt;:</span></span> Awesome I like, one of my fav</p>

                    <p className="log"><span className="stamp">8 hours ago <span className="username">&lt;Dart011&gt;:</span></span> Love the <a href="#">2:28</a> part, tune is great</p>

                    <p className="log"><span className="stamp">13 mins ago <span className="username">&lt;Screamo2&gt;:</span></span> Really good tunes, want to hear some more of that in next album! <a href="#">@2.27</a> was amazing</p>

                    <p className="log"><span className="stamp">2 hours ago <span className="username">&lt;Julio880&gt;:</span></span> Awesome I like, one of my fav</p>

                    <p className="log"><span className="stamp">8 hours ago <span className="username">&lt;Dart011&gt;:</span></span> Love the <a href="#">2:28</a> part, tune is great</p>
            </div>
        )
    }
}

ActivityLogContainer.PropTypes ={ 
    username: PropTypes.string.isRequired,
    userId: PropTypes.string.isRequired
}

export default ActivityLogContainer;