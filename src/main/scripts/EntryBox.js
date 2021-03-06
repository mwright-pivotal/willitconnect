"use strict";
import HeaderBar from './HeaderBar';
import EntryForm from './EntryForm';
import EntryList from './EntryList';
import React, { PropTypes } from 'react';

import { Grid, Row, Container } from 'react-bootstrap';

var EntryBox = React.createClass({
    getInitialState: function () {
        return {data: []};
    },
    handleEntrySubmit: function (entry) {
        var entries = this.state.data;
        var newEntries = [ entry ].concat( entries );
        this.setState({data: newEntries});
    },
    render: function () {
        mixpanel.track("page loaded");
        var bodyStyle = { 'padding' : 75};
        return (
            <Grid>
                <HeaderBar />
                <Row style={ bodyStyle }>
                    <EntryForm onEntrySubmit={this.handleEntrySubmit}/>
                    <EntryList data={this.state.data}/>
                </Row>
            </Grid>
        );
    }
});

module.exports = EntryBox;