import React, { Component, PropTypes } from 'react';
import Editor from 'draft-js-plugins-editor';
import createLinkifyPlugin from 'draft-js-linkify-plugin';
import createEmojiPlugin from 'draft-js-emoji-plugin';
import createMentionPlugin from 'draft-js-mention-plugin'
import { EditorState } from 'draft-js';
import { searchUser, getSearchQuery } from '../actions/search'
import { fromJS } from 'immutable';
import { connect } from 'react-redux';
import axios from 'axios';

import 'draft-js-emoji-plugin/lib/plugin.css';

const emojiPlugin = createEmojiPlugin();
const { EmojiSuggestions } = emojiPlugin;
const linkifyPlugin = createLinkifyPlugin({
    component: (props) => (
        <a {...props} onClick={() => console.log(props)} />
        ),
    target: '_blank'
});


const mentionPlugin = createMentionPlugin()

const { MentionSuggestions } = mentionPlugin;

const plugins = [
  //linkifyPlugin,
  emojiPlugin,
  mentionPlugin
  ];

class TFNEditor extends React.Component {
    
    state = {
        editorState: EditorState.createEmpty(),
        suggestions: fromJS([])
    }

    onChange = (editorState) => {
    this.setState({
        editorState,
        });
    };

    onSearchChange = ({value}) => {
        if(value.length > 1)
        {
            var promise = searchUser(value, this.props.token);
            console.log(promise);
                promise.then((result) => {
                    console.log(result.data)
                })
                .catch((error) => {
                    console.log(error)
                })  
        }
    }

    render() {
        return (
            <div onClick={this.focus}>
                <Editor
                editorState={this.state.editorState}
                onChange={this.onChange}
                plugins={plugins}
                ref={(element) => { this.editor = element; }}
                />
                <EmojiSuggestions />
                <MentionSuggestions
                    onSearchChange={this.onSearchChange}
                    suggestions={this.state.suggestions}
                />
            </div>
        )
    }
}

function mapStateToProps(state) {
    const { token } = state.token;
    return {
        token,
    }
}

export default connect(mapStateToProps)(TFNEditor);